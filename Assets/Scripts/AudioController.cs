
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	private void Start()
	{
	}

	public AudioSource AudioSource
	{
		get
		{
			AudioSource result;
			if ((result = this._audioSource) == null)
			{
				result = (this._audioSource = base.GetComponent<AudioSource>());
			}
			return result;
		}
	}

	public void PlayLoop(string audioName)
	{
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
		this.AudioSource.volume = 1f;
		if (!Preference.Instance.DataGame.IsMusic)
		{
			return;
		}
		if (!this.CheckClipExist(audioName))
		{
			return;
		}
		this.AudioSource.clip = this.AudioClips[audioName];
		this.AudioSource.loop = true;
		this.isPlaying = true;
		this.AudioSource.Play();
	}

	public void StopAllLoopSoundEffect()
	{
		this.SoundEffectLoop.Clear();
	}

	public void StopBackgroundMusic(string audioName)
	{
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
		float from = 100f;
		this._tween = DOTween.To(() => from, delegate(float x)
		{
			from = x;
		}, 0f, 0.5f).OnUpdate(delegate
		{
			this.AudioSource.volume = from / 100f;
		}).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.AudioSource.Stop();
			this.SoundEffectLoop.Remove(audioName);
			this.AudioSource.volume = 1f;
		});
	}

	public void StopLoopSoundEffect(string audioName)
	{
		this.SoundEffectLoop.Remove(audioName);
	}

	private IEnumerator _PlayLoopSoundEffect(string audioName)
	{
		if (!this.SoundEffectLoop.Contains(audioName))
		{
			yield break;
		}
		AudioClip clip = this.AudioClips[audioName];
		this.AudioSource.PlayOneShot(clip);
		yield return new WaitForSeconds(clip.length);
		base.StartCoroutine(this._PlayLoopSoundEffect(audioName));
		yield break;
	}

	public void PlayOneShot(string audioName)
	{
		if (!Preference.Instance.DataGame.IsSound)
		{
			return;
		}
		if (!this.CheckClipExist(audioName))
		{
			return;
		}
		if (audioName == "Audios/Effect/button_click_04")
		{
			if (!this.haveOtherSoundPlay)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.PlayButtonClick());
			}
		}
		else if (audioName == "Audios/Effect/ball_break_02")
		{
			if (!this.haveOtherSoundPlay)
			{
				this.haveOtherSoundPlay = true;
				this.AudioSource.PlayOneShot(this.AudioClips[audioName]);
				base.StopAllCoroutines();
				base.StartCoroutine(this.AfterOtherSoundPlay());
			}
		}
		else
		{
			this.haveOtherSoundPlay = true;
			this.AudioSource.PlayOneShot(this.AudioClips[audioName]);
			base.StopAllCoroutines();
			base.StartCoroutine(this.AfterOtherSoundPlay());
		}
	}

	private IEnumerator PlayButtonClick()
	{
		this.haveOtherSoundPlay = false;
		yield return new WaitForSeconds(this.timeWaitClickButton);
		if (!this.haveOtherSoundPlay)
		{
			this.AudioSource.PlayOneShot(this.AudioClips["Audios/Effect/button_click_04"]);
		}
		yield break;
	}

	private IEnumerator AfterOtherSoundPlay()
	{
		yield return new WaitForSeconds(this.timeWaitClickButton);
		this.haveOtherSoundPlay = false;
		yield break;
	}

	public bool CheckClipExist(string audioName)
	{
		if (this.AudioClips.ContainsKey(audioName))
		{
			return true;
		}
		AudioClip audioClip = Resources.Load<AudioClip>(audioName);
		if (audioClip == null)
		{
			return false;
		}
		this.AudioClips[audioName] = audioClip;
		return true;
	}

	public void PlayVibrate()
	{
		if (Preference.Instance.DataGame.IsVibrate)
		{
			//MMVibrationManager.Vibrate();
		}
	}

	public void PlayDeadVibrate()
	{
		if (Preference.Instance.DataGame.IsVibrate)
		{
			//MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
		}
	}

	private AudioSource _audioSource;

	[HideInInspector]
	public Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();

	[HideInInspector]
	public List<string> SoundEffectLoop = new List<string>();

	private bool isPlaying;

	private float timeWaitClickButton = 0.07f;

	private bool haveOtherSoundPlay;

	private Tween _tween;
}
