
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogDailyReward : Popup
{
	private void Start()
	{
		this.ButtonCollect.onClick.AddListener(delegate()
		{
			this.Hide();
			switch (this._currentDay)
			{
			case 0:
			case 1:
			case 2:
				GameController.ScreenManager.PlayController.MenuUI.StartAddCoinEffect(this._coinBonus[this._currentDay]);
				break;
			case 3:
			case 4:
			case 5:
			case 6:
				if (Preference.Instance.DataGame.CannonStatuses[this._currentDay].IsOpen)
				{
					GameController.ScreenManager.PlayController.MenuUI.StartAddCoinEffect(this._coinBonus[this._currentDay]);
				}
				else
				{
					Preference.Instance.DataGame.CannonStatuses[this._currentDay].NumTry += 2;
					GameController.DialogManager.DialogShopCanon.Show(this._currentDay);
				}
				break;
			}
		});
	}

	private void Update()
	{
	}

	public void Show(int day)
	{
		base.Show();
		this._currentDay = day;
		for (int i = 0; i < this.Days.Length; i++)
		{
			this.Days[i].Background.color = this.ColorNormal;
			this.Days[i].GoDone.SetActive(false);
			this.Days[i].Flash.SetActive(false);
		}
		for (int j = 0; j < day; j++)
		{
			this.Days[j].GoDone.SetActive(true);
		}
		for (int k = 3; k < 7; k++)
		{
			this.Days[k].GoCannon.SetActive(!Preference.Instance.DataGame.CannonStatuses[k].IsOpen);
			this.Days[k].GoCoin.SetActive(Preference.Instance.DataGame.CannonStatuses[k].IsOpen);
		}
		this.Days[this._currentDay].Background.color = this.ColorFocus;
		this.Days[this._currentDay].Flash.SetActive(true);
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
		Preference.Instance.DataGame.DailyRewardLastTime = DateTime.Now.Ticks;
		Preference.Instance.DataGame.DailyRewardCycleCount = (Preference.Instance.DataGame.DailyRewardCycleCount + 1) % 7;
	}

	public Day[] Days;

	private int _currentDay;

	public Button ButtonCollect;

	public Color ColorNormal;

	public Color ColorFocus;

	private int[] _coinBonus = new int[]
	{
		100,
		400,
		1000,
		1500,
		2000,
		2500,
		3000
	};
}
