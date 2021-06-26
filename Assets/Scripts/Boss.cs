
using System;
using System.Collections;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class Boss : BaseController
{
	public virtual void Start()
	{
		this._spwanCoin = false;
	}

	public virtual void Update()
	{
		if (base.gameObject.activeInHierarchy && GameController.ScreenManager.PlayController && GameController.ScreenManager.PlayController.GameStatus == PlayController.Game_Status.PLAYING)
		{
			this._time += Time.deltaTime;
			if (this._time >= this.TimeFire)
			{
				this._time = 0f;
				this.Fire();
			}
			this._timeSpawnBall += Time.deltaTime;
			if (this._timeSpawnBall >= this.TimeSpawnBall)
			{
				this._timeSpawnBall = 0f;
				this.SpawnBall();
			}
		}
	}

	public virtual void Fire()
	{
		base.StartCoroutine(this._Fire());
	}

	private IEnumerator _Fire()
	{
		this.SkeletonAnimation.AnimationState.SetAnimation(0, this.FireName, false);
		yield return new WaitForSeconds(this.SkeletonAnimation.AnimationState.GetCurrent(0).Animation.Duration);
		if (GameController.ScreenManager.PlayController.GameStatus == PlayController.Game_Status.PLAYING)
		{
			this.SkeletonAnimation.AnimationState.SetAnimation(0, this.IdleName, true);
		}
		yield break;
	}

	private IEnumerator _Die()
	{
		this.Rigidbody2D.velocity = Vector2.zero;
		this.SkeletonAnimation.AnimationState.SetAnimation(0, this.DieName, false);
		GameController.AudioController.PlayOneShot("Audios/Effect/monster");
		for (int i = 0; i < UnityEngine.Random.Range(20, 25); i++)
		{
			GameController.ScreenManager.PlayController.FrameGame.SpawnCoin(base.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)));
		}
		yield return new WaitForSeconds(1.9f);
		GameController.AudioController.PlayOneShot("Audios/Effect/boss_die");
		base.transform.DOMoveY(GameController.ScreenManager.PlayController.Canon.transform.position.y + 1.5f, 0.5f, false).OnComplete(delegate
		{
			Camera.main.DOShakePosition(0.2f, 0.2f, 10, 90f, true);
		});
		yield return new WaitForSeconds(1.8f);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	public void Bleed(float power)
	{
		if (GameController.ScreenManager.PlayController.GameStatus != PlayController.Game_Status.PLAYING)
		{
			return;
		}
		this.SetHearth(this.Hearth - power);
		this._damageHurt += Mathf.Min(power, this.Hearth);
		GameController.AudioController.PlayOneShot("Audios/Effect/ball_break_02");
		GameController.ScreenManager.PlayController.EffectController.StartBleed(this.BossHit);
		this.HitEffect();
		if (this._damageHurt >= (float)this.OrigilHearth / 100f)
		{
			GameController.ScreenManager.PlayController.EffectController.StartTextBlood((int)this._damageHurt, this);
			this._damageHurt = 0f;
		}
		if (!this._spwanCoin && this.Hearth < (float)(this.OrigilHearth / 2))
		{
			this._spwanCoin = true;
			GameController.AudioController.PlayOneShot("Audios/Effect/monster");
			for (int i = 0; i < UnityEngine.Random.Range(10, 15); i++)
			{
				GameController.ScreenManager.PlayController.FrameGame.SpawnCoin(base.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)));
			}
		}
	}

	public virtual void HitEffect()
	{
	}

	public void SetOrigilHearth(float hearth)
	{
		this.OrigilHearth = (int)hearth;
		this.SetHearth(hearth);
	}

	public void SetHearth(float hearth)
	{
		this.Hearth = hearth;
		if (this.Hearth <= 0f)
		{
			this.Hearth = 0f;
			base.StartCoroutine(this._Die());
			GameController.ScreenManager.PlayController.FrameGame.EndBoss();
		}
		GameController.ScreenManager.PlayController.PlayUI.UpdateProgress2(100f * (1f - hearth / (float)this.OrigilHearth));
	}

	public void SpawnBall()
	{
		if (GameController.ScreenManager.PlayController.FrameGame.Balls.Count >= 7)
		{
			return;
		}
		float num = (float)(Preference.Instance.DataGame.CurrentLevel + 1);
		float num2 = (float)Preference.Instance.DataGame.FireSpeed;
		float num3 = Mathf.Sqrt(Preference.Instance.DataGame.FirePower) - 1f + 0.1f;
		if (num3 < 0.7f)
		{
			num3 = 0.7f;
		}
		float num4 = 3f * Mathf.Sqrt(num2 * num) * num3 + num * num / 3f;
		if (Preference.Instance.DataGame.NumLose >= 25)
		{
			num4 = (float)((int)(num4 * 0.5f));
		}
		else if (Preference.Instance.DataGame.NumLose >= 20)
		{
			num4 = (float)((int)(num4 * 0.7f));
		}
		else if (Preference.Instance.DataGame.NumLose >= 15)
		{
			num4 = (float)((int)(num4 * 0.8f));
		}
		else if (Preference.Instance.DataGame.NumLose >= 10)
		{
			num4 = (float)((int)(num4 * 0.9f));
		}
		float num5 = UnityEngine.Random.Range(num, num4);
		int level = UnityEngine.Random.Range(0, 3);
		GameController.ScreenManager.PlayController.FrameGame.SpawnBallBoss((int)num5, level, this.BallPosition.position);
	}

	public virtual void StartMove()
	{
	}

	public virtual void Reset()
	{
	}

	public Transform BallPosition;

	public string IdleName;

	public string FireName;

	public string HitName;

	public string DieName;

	private float _time;

	private float _timeSpawnBall;

	public float TimeFire = 1f;

	public float TimeSpawnBall = 3.5f;

	public Rigidbody2D Rigidbody2D;

	public BossHit BossHit;

	public SkeletonAnimation SkeletonAnimation;

	[HideInInspector]
	public int OrigilHearth;

	[HideInInspector]
	public float Hearth;

	private float _damageHurt;

	private bool _spwanCoin;
}
