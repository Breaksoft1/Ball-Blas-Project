
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogSecondChance : Popup
{
	private void Start()
	{
		this.ButtonSecondChance.onClick.AddListener(new UnityAction(this.SecondChance));
	}

	private void Update()
	{
		if (!this._end)
		{
			this._time -= Time.deltaTime;
			this.TextTime.text = (int)this._time + string.Empty;
			if (this._time <= 0f)
			{
				this._time = 0f;
				this._end = true;
				this.Hide();
				GameController.ScreenManager.PlayController.GameOver();
			}
		}
	}

	public override void Show()
	{
		base.Show();
		this._end = false;
		this._time = 5f;
	}

	private void SecondChance()
	{
        this._end = true;
        this.Hide();
        GameController.ScreenManager.PlayController.SecondChance();
        GameController.AnalyticsController.LogEvent("use_second_chance", "level", (float)Preference.Instance.DataGame.CurrentLevel);
        //GameController.AdsController.ShowReward(delegate
        //{

        //});
    }

	public Text TextTime;

	public Button ButtonSecondChance;

	private bool _end;

	private float _time;
}
