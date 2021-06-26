
using System;
using DG.Tweening;
using UnityEngine;

public class BombBoss : BaseController
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if ((other.gameObject.tag.Equals("ground") || other.gameObject.tag.Equals("canon")) && base.gameObject.activeInHierarchy)
		{
			if (this._tween != null)
			{
				this._tween.Kill(false);
			}
			this._tween = this.SpriteRenderer.DOFade(0f, 0.2f).OnComplete(new TweenCallback(this.Despawwn));
			GameController.ScreenManager.PlayController.EffectController.StartBombEff(this.BombEff.position, this.BombEffect);
			Camera.main.DOShakePosition(0.2f, 0.06f, 10, 90f, true);
			GameController.AudioController.PlayOneShot("Audios/Effect/bomb");
		}
	}

	private void Despawwn()
	{
		TuNDPool.Despawn(base.gameObject);
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
		this.SpriteRenderer.color = Color.white;
		GameController.ScreenManager.PlayController.FrameGame.BombBosss.Remove(this);
	}

	public SpriteRenderer SpriteRenderer;

	public GameObject BombEffect;

	public Transform BombEff;

	private Tween _tween;
}
