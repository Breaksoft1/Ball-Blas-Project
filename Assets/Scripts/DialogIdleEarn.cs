
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogIdleEarn : Popup
{
	private void Start()
	{
		this.ButtonCollect.onClick.AddListener(delegate()
		{
			this.Hide();
			GameController.ScreenManager.PlayController.MenuUI.StartAddCoinEffect(this._coinEarn);
		});
		this.ButtonX3.onClick.AddListener(delegate()
		{
            this.Hide();
            GameController.ScreenManager.PlayController.MenuUI.StartAddCoinEffect(this._coinEarn * 3);
            GameController.AnalyticsController.LogEvent("x3_idle_earn");
            //GameController.AdsController.ShowReward(delegate
            //{
            //	this.Hide();
            //	GameController.ScreenManager.PlayController.MenuUI.StartAddCoinEffect(this._coinEarn * 3);
            //	GameController.AnalyticsController.LogEvent("x3_idle_earn");
            //});
        });
	}

	private void Update()
	{
	}

	public override void Show()
	{
		base.Show();
		double totalSeconds = TimeSpan.FromTicks(DateTime.Now.Ticks - Preference.Instance.DataGame.LastOnlineTime).TotalSeconds;
		float num = Preference.Instance.DataGame.CoinUpgradePower + Preference.Instance.DataGame.CoinUpgradeSpeed;
		num *= Preference.Instance.DataGame.FirePower;
		if (num > 5000f)
		{
			num = 5000f;
		}
		this._coinEarn = (int)(totalSeconds / 300.0 * (double)(Preference.Instance.DataGame.CurrentLevel + 1));
		this._coinEarn = (int)Mathf.Min((float)this._coinEarn, num);
		this.TextCoin.text = this._coinEarn + string.Empty;
		Preference.Instance.DataGame.LastOnlineTime = DateTime.Now.Ticks;
	}

	public Text TextCoin;

	public Button ButtonCollect;

	public Button ButtonX3;

	private int _coinEarn;
}
