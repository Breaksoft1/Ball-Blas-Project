
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogGameOver : Popup
{
	private void Start ()
	{
		this.ButtonContinue.onClick.AddListener (delegate() {
			this.Hide ();
			GameController.ScreenManager.PlayController.ContinueGameOver ();
            if (Preference.Instance.DataGame.CurrentLevel >= 2)
            {

            }
        });
		this.ButtonX2Coin.onClick.AddListener (delegate() {
            //GameController.AdsController.ShowReward(delegate
            //{
                this.Hide();
                GameController.ScreenManager.PlayController.ContinueGameOver();
                GameController.ScreenManager.PlayController.MenuUI.StartAddCoinEffect(GameController.ScreenManager.PlayController.CurrentCoin);
                GameController.AnalyticsController.LogEvent("user_x2_coin");
            });
        //});
	}

	private void Update ()
	{
	}

	public override void Show ()
	{
		base.Show ();
		if (!GameController.ScreenManager.PlayController.SurvivalMode) {
			GameController.ScreenManager.PlayController.CurrentCoin += (Preference.Instance.DataGame.CurrentLevel + 1) * 2;
			this.TextScore.text = GameController.ScreenManager.PlayController.PlayUI.Score + "%";
			Preference.Instance.DataGame.HighPercent = Mathf.Max (GameController.ScreenManager.PlayController.PlayUI.Score, Preference.Instance.DataGame.HighPercent);
			this.TextHightScore.text = Preference.Instance.DataGame.HighPercent + "%";
			this.TextCoin.text = FormatUtil.FormatMoneyDetail ((long)GameController.ScreenManager.PlayController.CurrentCoin);
		} else {
			this.TextScore.text = FormatUtil.FormatMoneyDetail ((long)GameController.ScreenManager.PlayController.PlayUI.Score);
			Preference.Instance.DataGame.HighScore = Mathf.Max (GameController.ScreenManager.PlayController.PlayUI.Score, Preference.Instance.DataGame.HighScore);
			this.TextHightScore.text = FormatUtil.FormatMoneyDetail ((long)Preference.Instance.DataGame.HighScore);
			this.TextCoin.text = FormatUtil.FormatMoneyDetail ((long)GameController.ScreenManager.PlayController.CurrentCoin);
		}
	}

	public Text TextScore;

	public Text TextHightScore;

	public Text TextCoin;

	public Button ButtonContinue;

	public Button ButtonX2Coin;
}
