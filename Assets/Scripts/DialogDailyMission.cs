
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogDailyMission : Popup
{
	private void Start()
	{
		this.ButtonClose.onClick.AddListener(new UnityAction(this.Hide));
		for (int i = 0; i < this.MissionItems.Length; i++)
		{
			int i1 = i;
			this.MissionItems[i].ButtonCollect.onClick.AddListener(delegate()
			{
				if (this.MissionItems[i1].ButtonCollect.GetComponent<CanvasGroup>().alpha >= 1f)
				{
					Preference.Instance.DataGame.DoneMi[i1] = true;
					this.MissionItems[i1].ButtonCollect.gameObject.SetActive(false);
					Preference.Instance.DataGame.Coin += this._reward;
					this.StartCoinEffect(this.MissionItems[i1].ButtonCollect.transform.position);
					this.CheckDone();
					GameController.ScreenManager.PlayController.MenuUI.UpdateMission();
				}
			});
		}
		this.Button2.onClick.AddListener(delegate()
		{
			if (this.Button2.GetComponent<CanvasGroup>().alpha >= 1f)
			{
				Preference.Instance.DataGame.Coin += this._reward2Day;
				Preference.Instance.DataGame.DoneMi2 = true;
				this.Button2.gameObject.SetActive(false);
				this.StartCoinEffect(this.Button2.transform.position);
				GameController.ScreenManager.PlayController.MenuUI.UpdateMission();
			}
		});
		this.Button4.onClick.AddListener(delegate()
		{
			if (this.Button4.GetComponent<CanvasGroup>().alpha >= 1f)
			{
				Preference.Instance.DataGame.Coin += this._reward4Day;
				this.StartCoinEffect(this.Button4.transform.position);
				Preference.Instance.DataGame.DoneMi4 = true;
				this.Button4.gameObject.SetActive(false);
				GameController.ScreenManager.PlayController.MenuUI.UpdateMission();
			}
		});
	}

	private void Update()
	{
	}

	public override void Show()
	{
		base.Show();
		this._reward = (int)Preference.Instance.DataGame.CoinUpgradePower / 10 * 10;
		this._reward2Day = this._reward * 2;
		this._reward4Day = this._reward2Day * 2;
		this.MissionItems[0].SetValue(Preference.Instance.DataGame.NumBallShoot, Preference.Instance.DataGame.DoneMi[0]);
		this.MissionItems[1].SetValue(Preference.Instance.DataGame.NumUpgradeSpeed, Preference.Instance.DataGame.DoneMi[1]);
		this.MissionItems[2].SetValue(Preference.Instance.DataGame.NumPlay, Preference.Instance.DataGame.DoneMi[2]);
		this.MissionItems[3].SetValue(Preference.Instance.DataGame.NumBossKill, Preference.Instance.DataGame.DoneMi[3]);
		for (int i = 0; i < this.MissionItems.Length; i++)
		{
			this.MissionItems[i].ButtonCollect.GetComponentInChildren<Text>().text = FormatUtil.FormatMoney((long)this._reward);
		}
		this.Button2.GetComponentInChildren<Text>().text = FormatUtil.FormatMoney((long)this._reward2Day);
		this.Button4.GetComponentInChildren<Text>().text = FormatUtil.FormatMoney((long)this._reward4Day);
		this.CheckDone();
		GameController.AnalyticsController.LogEvent("click_daily_mission");
	}

	public void CheckDone()
	{
		int num = 0;
		for (int i = 0; i < Preference.Instance.DataGame.DoneMi.Length; i++)
		{
			if (Preference.Instance.DataGame.DoneMi[i])
			{
				num++;
			}
		}
		this.Button4.GetComponent<CanvasGroup>().alpha = 0.6f;
		this.Button2.GetComponent<CanvasGroup>().alpha = 0.6f;
		this.Button2.GetComponent<Image>().color = Color.gray;
		this.Button4.GetComponent<Image>().color = Color.gray;
		if (num == 4)
		{
			this.Button4.GetComponent<CanvasGroup>().alpha = 1f;
			this.Button2.GetComponent<CanvasGroup>().alpha = 1f;
			this.Button2.GetComponent<Image>().color = Color.white;
			this.Button4.GetComponent<Image>().color = Color.white;
		}
		else if (num >= 2)
		{
			this.Button2.GetComponent<CanvasGroup>().alpha = 1f;
			this.Button2.GetComponent<Image>().color = Color.white;
		}
		for (int j = 0; j < this.ImageStep.Length; j++)
		{
			this.ImageStep[j].color = this.ColorBlack;
		}
		for (int k = 0; k < num; k++)
		{
			this.ImageStep[k].color = this.ColorGreen;
		}
		this.Button2.gameObject.SetActive(!Preference.Instance.DataGame.DoneMi2);
		this.Button4.gameObject.SetActive(!Preference.Instance.DataGame.DoneMi4);
	}

	public void StartCoinEffect(Vector2 position)
	{
		GameController.ScreenManager.PlayController.MenuUI.UpgradePrice();
		GameController.AudioController.PlayOneShot("Audios/Effect/coin_fly");
		for (int i = 0; i < 10; i++)
		{
			Image image = base.CreateImage(this.SpriteCoins[UnityEngine.Random.Range(0, this.SpriteCoins.Length)], base.transform);
			image.transform.position = position;
			image.transform.localScale = Vector3.one * 0.85f;
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

	public MissionItem[] MissionItems;

	public Image[] ImageStep;

	public Button Button2;

	public Button Button4;

	public Color ColorBlack;

	public Color ColorGreen;

	public Button ButtonClose;

	public Sprite[] SpriteCoins;

	private int _reward2Day;

	private int _reward4Day;

	private int _reward;
}
