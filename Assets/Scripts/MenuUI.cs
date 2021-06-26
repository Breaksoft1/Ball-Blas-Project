
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuUI : GameState
{
	public override void Start ()
	{
		base.Start ();
		this.ButonUpgradeSpeed.onClick.AddListener (delegate() {
			if ((int)Preference.Instance.DataGame.CoinUpgradeSpeed <= Preference.Instance.DataGame.Coin) {
				Preference.Instance.DataGame.FireSpeed++;
				Preference.Instance.DataGame.Coin -= (int)Preference.Instance.DataGame.CoinUpgradeSpeed;
				Preference.Instance.DataGame.CoinUpgradeSpeed += Preference.Instance.DataGame.CoinUpgradeSpeed * 0.1f;
				this.UpgradePrice ();
				GameController.AnalyticsController.LogEvent ("upgrade_speed", "speed", (float)Preference.Instance.DataGame.FireSpeed);
				GameController.AudioController.PlayOneShot ("Audios/Effect/levelup");
				GameController.ScreenManager.PlayController.Canon.PowerUp ();
				Preference.Instance.DataGame.NumUpgradeSpeed++;
				this.UpdateMission ();
			}
			if (this.ImageTutUpgrade.gameObject.activeInHierarchy) {
				this.ImageTutUpgrade.gameObject.SetActive (false);
			}
		});
		this.ButtonUpgradePower.onClick.AddListener (delegate() {
			if ((int)Preference.Instance.DataGame.CoinUpgradePower <= Preference.Instance.DataGame.Coin) {
				Preference.Instance.DataGame.FirePower += 0.1f;
				Preference.Instance.DataGame.Coin -= (int)Preference.Instance.DataGame.CoinUpgradePower;
				Preference.Instance.DataGame.CoinUpgradePower += Preference.Instance.DataGame.CoinUpgradePower * 0.1f;
				this.UpgradePrice ();
				GameController.AnalyticsController.LogEvent ("upgrade_power", "power", Preference.Instance.DataGame.FirePower);
				GameController.AudioController.PlayOneShot ("Audios/Effect/levelup");
				GameController.ScreenManager.PlayController.Canon.PowerUp ();
			}
			if (this.ImageTutUpgrade.gameObject.activeInHierarchy) {
				this.ImageTutUpgrade.gameObject.SetActive (false);
			}
		});
		this.ButtonShop.onClick.AddListener (delegate() {
			GameController.DialogManager.DialogShop.Show ();
		});
		this.ButtonShopCanon.onClick.AddListener (new UnityAction (GameController.DialogManager.DialogShopCanon.Show));
		this.ButtonSetting.onClick.AddListener (new UnityAction (GameController.DialogManager.DialogSetting.Show));
		this.UpgradePrice ();
		this.ButtonOffer.transform.eulerAngles = new Vector3 (0f, 0f, 0f);
		DOTween.Sequence ().AppendInterval (1f).Append (this.ButtonOffer.transform.DORotate (new Vector3 (0f, 0f, -10f), 0.1f, RotateMode.Fast)).Append (this.ButtonOffer.transform.DORotate (new Vector3 (0f, 0f, 10f), 0.2f, RotateMode.Fast)).Append (this.ButtonOffer.transform.DORotate (new Vector3 (0f, 0f, 0f), 0.1f, RotateMode.Fast)).SetLoops (-1);
		this.ButtonOffer.onClick.AddListener (delegate() {
			GameController.DialogManager.DialogOffer.Show ();
			GameController.AnalyticsController.LogEvent ("click_offer");
		});
		//this.ButtonRemoveAd.onClick.AddListener (delegate() {
		//	GameController.PurchaseController.BuyProductID (PurchaseController.ProductRemoveAd);
		//});
		//this.ButtonRank.onClick.AddListener (ShowLeaderBoard);
		this.ButtonDailyMission.onClick.AddListener (new UnityAction (GameController.DialogManager.DialogDailyMission.Show));
        //this.ButtonRank.gameObject.SetActive(true);
    }

	private void ShowLeaderBoard ()
	{

    }

	public void UpgradePrice ()
	{
		this.SetPriceUpgradeSpeed ((int)Preference.Instance.DataGame.CoinUpgradeSpeed);
		this.SetPriceUpgradePower ((int)Preference.Instance.DataGame.CoinUpgradePower);
	}

	private void Update ()
	{
		this.TextFireSpeed.text = Preference.Instance.DataGame.FireSpeed + " bps";
		this.TextFirePower.text = Math.Round ((double)(Preference.Instance.DataGame.FirePower * 100f)) + "%";
		this.TextCoin.text = FormatUtil.FormatMoney ((long)Preference.Instance.DataGame.Coin);
	}

	public void StartGame ()
	{
		if (GameState.PlayController.GameStatus == PlayController.Game_Status.START) {
			GameState.PlayController.StartGame ();
		}
	}

	public override void Open ()
	{
		base.Open ();
		this.TextLevel.transform.parent.gameObject.SetActive (!GameState.PlayController.SurvivalMode);
		this.TextHighScore.transform.parent.gameObject.SetActive (GameState.PlayController.SurvivalMode);
		this.ModeGame.gameObject.SetActive (Preference.Instance.DataGame.CurrentLevel > 6);
		this.ModeGame.CheckFocus ();
		this.TextLevel.text = "Lv " + (Preference.Instance.DataGame.CurrentLevel + 1);
		this.ModeGame.TextLevel.text = "Lv " + (Preference.Instance.DataGame.CurrentLevel + 1);
		this.ModeGame.TextHighScore.text = string.Empty + FormatUtil.FormatHighScore ((long)Preference.Instance.DataGame.HighScore);
		this.TextHighScore.text = string.Empty + FormatUtil.FormatHighScore ((long)Preference.Instance.DataGame.HighScore);
		//this.ButtonRemoveAd.gameObject.SetActive (!Preference.Instance.DataGame.NoAds);
		this.UpgradePrice ();
		if (Preference.Instance.DataGame.CannonStatuses [3].IsOpen || Preference.Instance.DataGame.CannonStatuses [4].IsOpen || Preference.Instance.DataGame.CannonStatuses [5].IsOpen || Preference.Instance.DataGame.CannonStatuses [6].IsOpen) {
			this.ButtonOffer.gameObject.SetActive (false);
		} else {
			this.ButtonOffer.gameObject.SetActive (Preference.Instance.DataGame.CurrentLevel >= 4);
		}
		if (GameState.PlayController.Tutorial) {
			GameState.PlayController.Tutorial = false;
			this.ShowTutorial ();
		}
		this.UpdateMission ();
	}

	public void SetPriceUpgradeSpeed (int price)
	{
		this.ButonUpgradeSpeed.GetComponentInChildren<Text> ().text = FormatUtil.FormatMoney ((long)price);
		if (price > Preference.Instance.DataGame.Coin) {
			this.ButonUpgradeSpeed.GetComponent<Image> ().color = Color.gray;
			this.ButonUpgradeSpeed.GetComponentInChildren<Text> ().color = new Color (1f, 1f, 1f, 0.5f);
			this.ImageCoinSpeed.color = new Color (1f, 1f, 1f, 0.5f);
			this.FlashSpeed.gameObject.SetActive (false);
		} else {
			this.ButonUpgradeSpeed.GetComponent<Image> ().color = Color.white;
			this.ButonUpgradeSpeed.GetComponentInChildren<Text> ().color = new Color (1f, 1f, 1f, 1f);
			this.ImageCoinSpeed.color = new Color (1f, 1f, 1f, 1f);
			this.FlashSpeed.gameObject.SetActive (true);
		}
	}

	public void SetPriceUpgradePower (int price)
	{
		this.ButtonUpgradePower.GetComponentInChildren<Text> ().text = FormatUtil.FormatMoney ((long)price);
		if (price > Preference.Instance.DataGame.Coin) {
			this.ButtonUpgradePower.GetComponent<Image> ().color = Color.gray;
			this.ButtonUpgradePower.GetComponentInChildren<Text> ().color = new Color (1f, 1f, 1f, 0.5f);
			this.ImageCoinPower.color = new Color (1f, 1f, 1f, 0.5f);
			this.FlashPower.gameObject.SetActive (false);
		} else {
			this.ButtonUpgradePower.GetComponent<Image> ().color = Color.white;
			this.ButtonUpgradePower.GetComponentInChildren<Text> ().color = new Color (1f, 1f, 1f, 1f);
			this.ImageCoinPower.color = new Color (1f, 1f, 1f, 1f);
			this.FlashPower.gameObject.SetActive (true);
		}
	}

	public void StartAddCoinEffect (int coin)
	{
		GameController.AudioController.PlayOneShot ("Audios/Effect/coin_fly");
		for (int i = 0; i < UnityEngine.Random.Range (20, 30); i++) {
			int i1 = i;
			Image image = base.CreateImage (this.CoinSprites [UnityEngine.Random.Range (0, this.CoinSprites.Length)], base.transform);
			image.transform.localScale = Vector3.one * 0.85f;
			image.transform.localPosition = Vector3.zero;
			image.transform.DOLocalMove (new Vector2 (UnityEngine.Random.Range (-base.GetComponent<RectTransform> ().rect.width / 2f, base.GetComponent<RectTransform> ().rect.width / 2f), UnityEngine.Random.Range (-base.GetComponent<RectTransform> ().rect.height / 3f, base.GetComponent<RectTransform> ().rect.height / 3f)), UnityEngine.Random.Range (0.2f, 0.4f), false).OnComplete (delegate {
				float duration = UnityEngine.Random.Range (0.3f, 0.6f);
				image.DOFade (0.2f, duration).SetEase (Ease.InQuart).SetDelay (0.3f);
				image.transform.DOMove (this.ImageMyCoin.transform.position, duration, false).OnComplete (delegate {
					UnityEngine.Object.Destroy (image);
					if (i1 == 0) {
						Preference.Instance.DataGame.Coin += coin;
						this.UpgradePrice ();
					}
				}).SetDelay (0.3f);
			});
		}
	}

	public void ShowTutorial ()
	{
		if ((int)Preference.Instance.DataGame.CoinUpgradeSpeed <= Preference.Instance.DataGame.Coin) {
			this.ImageTutUpgrade.gameObject.SetActive (true);
			this.ImageArrow.transform.DOLocalMoveY (this.ImageArrow.transform.localPosition.y - 10f, 0.3f, false).SetLoops (-1, LoopType.Yoyo);
		} else {
			GameState.PlayController.Tutorial = false;
		}
	}

	public void UpdateMission ()
	{
		bool active = false;
		if (Preference.Instance.DataGame.NumBallShoot >= 500 && !Preference.Instance.DataGame.DoneMi [0]) {
			active = true;
		}
		if (Preference.Instance.DataGame.NumUpgradeSpeed >= 10 && !Preference.Instance.DataGame.DoneMi [1]) {
			active = true;
		}
		if (Preference.Instance.DataGame.NumPlay >= 50 && !Preference.Instance.DataGame.DoneMi [2]) {
			active = true;
		}
		if (Preference.Instance.DataGame.NumBossKill >= 1 && !Preference.Instance.DataGame.DoneMi [3]) {
			active = true;
		}
		this.ImageDot.gameObject.SetActive (active);
	}

	public Sprite[] CoinSprites;

	public Image ImageMyCoin;

	public GameObject FlashSpeed;

	public GameObject FlashPower;

	public Text TextFireSpeed;

	public Image ImageCoinSpeed;

	public Text TextFirePower;

	public Image ImageCoinPower;

	public Button ButonUpgradeSpeed;

	public Button ButtonUpgradePower;

	public Button ButtonShop;

	//public Button ButtonRemoveAd;

	public Button ButtonSetting;

	public Button ButtonShopCanon;

	public Button ButtonOffer;

	//public Button ButtonRank;

	public Button ButtonDailyMission;

	public Text TextLevel;

	public Text TextHighScore;

	public Text TextCoin;

	public Text TextSwipeToPlay;

	public Image ImageTutUpgrade;

	public Image ImageArrow;

	public Image ImageDot;

	public ModeGame ModeGame;
}
