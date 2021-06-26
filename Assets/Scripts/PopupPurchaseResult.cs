
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupPurchaseResult : Popup
{
	private void Start()
	{
		this.ButtonDone.onClick.AddListener(new UnityAction(this.Hide));
	}

	private void Update()
	{
	}

	public void ShowFail()
	{
		this._iscoin = false;
		base.Show();
		this.TextContent.text = "Purchase Fail";
		this.Coin.gameObject.SetActive(false);
	}

	public void ShowSuccessRemoveAds()
	{
		this._iscoin = false;
		base.Show();
		this.TextContent.text = "Congrat! Ads have been removed";
		this.Coin.gameObject.SetActive(false);
		Preference.Instance.DataGame.NoAds = true;
		//GameController.AdsController.DestroyBanner();
		GameController.AnalyticsController.LogEvent(AnalyticsController.PURCHASE_REMOVE_ADS);
	}

	public void ShowSuccess()
	{
		this._iscoin = false;
		base.Show();
		this.TextContent.text = "Congrat! Purchase success";
		this.Coin.gameObject.SetActive(false);
		Preference.Instance.DataGame.NoAds = true;
		//GameController.AdsController.DestroyBanner();
		GameController.AnalyticsController.LogEvent(AnalyticsController.PURCHASE_REMOVE_ADS);
	}

	public void ShowSuccessCoin(int coin)
	{
		this._iscoin = true;
		base.Show();
		Preference.Instance.DataGame.Coin += coin;
		this.TextContent.text = "You buy success <color=yellow>" + coin + " </color>coin.";
		this.Coin.gameObject.SetActive(true);
		Preference.Instance.DataGame.NoAds = true;
		//GameController.AdsController.DestroyBanner();
		GameController.AnalyticsController.LogEvent(AnalyticsController.PURCHASE, AnalyticsController.PACKAGE, "Coin " + coin);
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
		if (this._iscoin)
		{
			this.StartCoinEff();
		}
	}

	public void StartCoinEff()
	{
		Sprite sprite = Resources.Load<Sprite>("Images/UI/diamond");
		for (int i = 0; i < 10; i++)
		{
			Image image = base.CreateImage(sprite, base.transform);
			image.transform.position = this.Coin.transform.position;
			float time = UnityEngine.Random.Range(0.4f, 0.6f);
			float x = image.rectTransform.anchoredPosition.x + (float)UnityEngine.Random.Range(-50, 50);
			float y = image.rectTransform.anchoredPosition.y + (float)UnityEngine.Random.Range(-20, 150);
			DOTween.Sequence().AppendCallback(delegate
			{
				image.transform.DORotate(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), time, RotateMode.Fast).SetEase(Ease.Linear);
				image.rectTransform.DOAnchorPos(new Vector2(x, y), time, false).SetEase(Ease.Linear);
				image.DOFade(0.3f, time).SetEase(Ease.Linear);
			}).AppendInterval(time * 2f / 3f).Append(image.DOFade(0f, 0.2f).SetEase(Ease.Linear)).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(image.gameObject);
			});
		}
	}

	public Text TextContent;

	public Image Coin;

	public Button ButtonDone;

	private bool _iscoin;
}
