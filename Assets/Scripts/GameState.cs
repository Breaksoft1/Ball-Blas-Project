
using System;
using DG.Tweening;
using UnityEngine;

public class GameState : BaseController
{
	public static PlayController PlayController
	{
		get
		{
			if (GameState._playController == null)
			{
				GameState._playController = UnityEngine.Object.FindObjectOfType<PlayController>();
			}
			return GameState._playController;
		}
	}

	public virtual void Start()
	{
		if (this.Top)
		{
			this._topPos = this.Top.localPosition;
			this.Top.localPosition = new Vector2(this._topPos.x, this._topPos.y + this.Top.rect.height);
		}
		if (this.Bottom)
		{
			this._bottomPos = this.Bottom.localPosition;
			this.Bottom.localPosition = new Vector2(this._bottomPos.x, this._bottomPos.y - this.Bottom.rect.height);
		}
		if (this.Center)
		{
			this.Center.GetComponent<CanvasGroup>().alpha = 0f;
		}
	}

	private void Update()
	{
	}

	public virtual void Open()
	{
		GameController.AudioController.PlayOneShot("Audios/Effect/appear");
		if (this._tweenTop != null)
		{
			this._tweenTop.Kill(false);
		}
		if (this._tweenBot != null)
		{
			this._tweenBot.Kill(false);
		}
		if (this._tweenCenter != null)
		{
			this._tweenCenter.Kill(false);
		}
		base.gameObject.SetActive(true);
		if (this.Top)
		{
			this._tweenTop = this.Top.DOLocalMoveY(this._topPos.y, this._timeM, false).SetEase(Ease.OutQuad);
		}
		if (this.Bottom)
		{
			this._tweenBot = this.Bottom.DOLocalMoveY(this._bottomPos.y, this._timeM, false).SetEase(Ease.OutQuad);
		}
		if (this.Center)
		{
			this._tweenCenter = this.Center.GetComponent<CanvasGroup>().DOFade(1f, this._timeM);
		}
	}

	public virtual void Close()
	{
		if (this._tweenTop != null)
		{
			this._tweenTop.Kill(false);
		}
		if (this._tweenBot != null)
		{
			this._tweenBot.Kill(false);
		}
		if (this._tweenCenter != null)
		{
			this._tweenCenter.Kill(false);
		}
		if (this.Top)
		{
			this._tweenTop = this.Top.DOLocalMoveY(this._topPos.y + this.Top.rect.height, this._timeM, false).SetEase(Ease.OutQuad).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
			});
		}
		if (this.Bottom)
		{
			this._tweenBot = this.Bottom.DOLocalMoveY(this._bottomPos.y - this.Bottom.rect.height, this._timeM, false).SetEase(Ease.OutQuad);
		}
		if (this.Center)
		{
			this._tweenCenter = this.Center.GetComponent<CanvasGroup>().DOFade(0f, this._timeM);
		}
	}

	public RectTransform Top;

	public RectTransform Bottom;

	public RectTransform Center;

	private static PlayController _playController;

	private Vector2 _topPos;

	private Vector2 _bottomPos;

	private float _timeM = 0.3f;

	private Tween _tweenTop;

	private Tween _tweenBot;

	private Tween _tweenCenter;
}
