
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Popup : BaseController
{
	private void Awake()
	{
		this._blurColor = this.BackgroundBlur.color;
	}

	private void Update()
	{
	}

	public virtual void Show()
	{
		base.transform.SetAsLastSibling();
		this.StopAllTweens();
		base.gameObject.SetActive(true);
		this.BackgroundBlur.color = new Color(this._blurColor.r, this._blurColor.g, this._blurColor.b, 0f);
		this._backgroundBlurTweener = this.BackgroundBlur.DOFade(this._blurColor.a, this._timeOpen).OnComplete(new TweenCallback(this.OnShowComplete));
		this.Group.anchoredPosition = new Vector2(-base.GetComponent<RectTransform>().rect.width / 2f - this.Group.rect.width / 2f, 0f);
		this.Group.DOAnchorPosX(0f, this._timeOpen, false).SetEase(Ease.OutBack);
        if (!(this is Toast) && !(this is DialogDailyReward))
        {
            //GameController.AdsController.SetBannerShow(true);
        }
    }

    public virtual void Hide()
	{
		this.StopAllTweens();
		this.Group.DOAnchorPosX(base.GetComponent<RectTransform>().rect.width / 2f + this.Group.rect.width / 2f, this._timeClose, false).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
			if (GameController.ScreenManager.currentStage is PlayController && !Preference.Instance.DataGame.NoAds && GameController.DialogManager.GetNumberActiveDialog() == 0)
			{
                //GameController.AdsController.SetBannerShow(false);
            }
		});
		this._backgroundBlurTweener = this.BackgroundBlur.DOFade(0f, this._timeClose);
	}

	private void StopAllTweens()
	{
		if (this._backgroundBlurTweener != null)
		{
			this._backgroundBlurTweener.Kill(false);
		}
	}

	public virtual void OnShowComplete()
	{
	}

	public RectTransform Group;

	public Image BackgroundBlur;

	private Tweener _backgroundBlurTweener;

	private Color _blurColor;

	private float _timeOpen = 0.35f;

	private float _timeClose = 0.25f;
}
