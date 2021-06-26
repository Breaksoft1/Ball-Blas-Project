
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetLevel(int level)
	{
		Preference.Instance.DataGame.CurrentLevel = level;
		this.TextCurrentLevel.text = level + 1 + string.Empty;
		this.TextNextLevel.text = level + 2 + string.Empty;
	}

	public void SetMaxBall(int maxBall)
	{
		this._numberOfBallToSpawn = maxBall;
		this.Progress.DOValue(0f, 0.3f, false);
	}

	public void UpdateProgress(int ball)
	{
		if (this._numberOfBallToSpawn != 0)
		{
			this.Progress.DOValue((float)(ball + 1) * 100f / (float)this._numberOfBallToSpawn, 0.3f, false);
		}
	}

	public Text TextCurrentLevel;

	public Text TextNextLevel;

	public Text TextScore;

	public Slider Progress;

	private int _numberOfBallToSpawn;
}
