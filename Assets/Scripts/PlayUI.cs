
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : GameState
{
	public override void Start()
	{
		base.Start();
	}

	private void Update()
	{
		this.TextCoin.text = FormatUtil.FormatMoney((long)Preference.Instance.DataGame.Coin);
	}

	public void SetLevel(int level)
	{
		if (!GameState.PlayController.SurvivalMode)
		{
			Preference.Instance.DataGame.CurrentLevel = level;
			this.TextCurrentLevel.text = "Lv " + (level + 1) + string.Empty;
			this.TextNextLevel.text = level + 2 + string.Empty;
			this.Progress.value = 0f;
			this.SetPercent(0);
			this.TextCurrentLevel.transform.parent.gameObject.SetActive(true);
		}
		else
		{
			this.Score = 0;
			this.AddScore(0);
			this.TextCurrentLevel.transform.parent.gameObject.SetActive(false);
		}
	}

	public override void Open()
	{
		base.Open();
		this.TextCurrentLevel.transform.parent.gameObject.SetActive(!GameState.PlayController.SurvivalMode);
		this.TextHighScore.transform.parent.gameObject.SetActive(GameState.PlayController.SurvivalMode);
		this.Progress.gameObject.SetActive(!GameState.PlayController.SurvivalMode);
	}

	public void SetMaxBall(int maxBall)
	{
		this._numberOfBallToSpawn = maxBall;
		this.SetPercent(0);
		this.Progress.DOValue(0f, 0.3f, false);
	}

	public void UpdateProgress(int ball)
	{
		if (this._numberOfBallToSpawn != 0)
		{
			this.Progress.DOValue((float)(ball + 1) * 100f / (float)this._numberOfBallToSpawn, 0.3f, false);
			this.SetPercent(Mathf.Min((ball + 1) * 100 / this._numberOfBallToSpawn, 99));
		}
	}

	public void UpdateProgress2(float value)
	{
		this.Progress.DOValue(value, 0.3f, false);
		this.SetPercent((int)value);
	}

	public void SetPercent(int score)
	{
		if (score > 100)
		{
			score = 100;
		}
		this.TextScore.text = score + "%";
		this.Score = score;
	}

	public void AddScore(int score)
	{
		this.Score += score;
		this.TextScore.text = FormatUtil.FormatMoneyDetail((long)this.Score);
		Preference.Instance.DataGame.HighScore = Mathf.Max(Preference.Instance.DataGame.HighScore, this.Score);
		this.TextHighScore.text = string.Empty + FormatUtil.FormatHighScore((long)Preference.Instance.DataGame.HighScore);
	}

	public Text TextCurrentLevel;

	public Text TextNextLevel;

	public Text TextScore;

	public Text TextHighScore;

	public Text TextCoin;

	public Slider Progress;

	private int _numberOfBallToSpawn;

	public int Score;
}
