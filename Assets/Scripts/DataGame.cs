
using System;
using System.Collections.Generic;

[Serializable]
public class DataGame
{
	public DataGame()
	{
		this.IsLoginGG = false;
		this.RateUs = false;
		this.CurrenCanon = 0;
		this.FirstDie = false;
		this.NoAds = false;
		this.IsMusic = true;
		this.IsSound = true;
		this.IsVibrate = true;
		this.FirstOpen = true;
		this.FirstChangeMode = true;
		this.DailyRewardStatus = new List<bool>();
		this.CannonStatuses = new CannonStatus[10];
		for (int i = 0; i < this.CannonStatuses.Length; i++)
		{
			this.CannonStatuses[i] = new CannonStatus();
			this.CannonStatuses[i].Id = i;
			this.CannonStatuses[i].IsOpen = false;
			this.CannonStatuses[i].NumTry = 0;
		}
		this.CannonStatuses[0].IsOpen = true;
		this.CannonStatuses[1].IsOpen = true;
		this.Coin = 0;
		this.FireSpeed = 5;
		this.FirePower = 1f;
		this.CurrentLevel = 0;
		this.CoinUpgradeSpeed = 20f;
		this.CoinUpgradePower = 20f;
		this.DoneMi = new bool[4];
	}

	public int CoinDrop
	{
		get
		{
			float num = (float)this.FireSpeed / 10f;
			int num2 = (int)((this.FirePower + num + 0.6f) * 0.75f);
			if ((float)num2 < this.FirePower)
			{
				num2 = (int)this.FirePower;
			}
			if (num2 < 1)
			{
				num2 = 1;
			}
			return num2;
		}
	}

	public void ResetDailyReward()
	{
		this.NumBallShoot = 0;
		this.NumUpgradeSpeed = 0;
		this.NumBossKill = 0;
		this.NumPlay = 0;
		for (int i = 0; i < this.DoneMi.Length; i++)
		{
			this.DoneMi[i] = false;
		}
		this.DoneMi2 = false;
		this.DoneMi4 = false;
	}

	public int CurrenCanon;

	public bool NoAds;

	public bool IsMusic;

	public bool IsSound;

	public bool IsVibrate;

	public bool FirstDie;

	public bool FirstChangeMode;

	public bool RateUs;

	public bool IsLoginGG;

	public CannonStatus[] CannonStatuses;

	public int Coin;

	public float CoinUpgradeSpeed;

	public float CoinUpgradePower;

	public bool FirstOpen;

	public List<bool> DailyRewardStatus;

	public int LastDay;

	public long LastOnlineTime;

	public int DailyRewardCycleCount;

	public long DailyRewardLastTime;

	public int NumLose;

	public int NumBallShoot;

	public int NumUpgradeSpeed;

	public int NumBossKill;

	public int NumPlay;

	public bool[] DoneMi = new bool[4];

	public bool DoneMi2;

	public bool DoneMi4;

	public int HighScore;

	public int HighPercent;

	public int CurrentLevel;

	public float FirePower;

	public int FireSpeed;
}
