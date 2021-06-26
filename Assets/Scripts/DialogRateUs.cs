
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogRateUs : Popup
{
	private void Start()
	{
		this.Button14Stars.onClick.AddListener(delegate()
		{
			GameController.AnalyticsController.LogEvent("rate_14_star");
			this.Hide();
		});
		this.ButtonClose.onClick.AddListener(delegate()
		{
			GameController.AnalyticsController.LogEvent("rate_close");
			this.Hide();
		});
		this.Button5Stars.onClick.AddListener(delegate()
		{
			this.Hide();
			GameController.AnalyticsController.LogEvent("rate_5_star");
			Application.OpenURL(BaseController.GameController.LinkGame());
		});
	}

	private void Update()
	{
	}

	public Button Button14Stars;

	public Button ButtonClose;

	public Button Button5Stars;
}
