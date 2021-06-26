
using System;
using System.Collections;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
	private void Start()
	{
		this._difficultNumber = 8;
	}

	public void Spawn()
	{
		this.CurrentBallIndex = 0;
		this.SetLevelStats();
		base.StopAllCoroutines();
		base.StartCoroutine(this.SpawnBall());
	}

	public void SetLevelStats()
	{
		this._currentDamage = Preference.Instance.DataGame.FirePower;
		this._currentBulletPerSecond = Preference.Instance.DataGame.FireSpeed;
		this._currentLevel = Preference.Instance.DataGame.CurrentLevel + 1;
		if (this._currentLevel == 1)
		{
			this.waitTime = 2.5f;
		}
		else if (this._currentLevel == 2)
		{
			this.waitTime = 3f;
		}
		else
		{
			this.waitTime = 3.5f;
		}
		int num = 5;
		int num2 = 20;
		int num3 = this._currentLevel - this._currentLevel / 10 * 10;
		int num4 = num3 + num;
		if (num4 >= num2)
		{
			this.NumberOfBallToSpawn = num2;
		}
		else
		{
			this.NumberOfBallToSpawn = num4;
		}
		this._normalSpawnBall = new int[this.NumberOfBallToSpawn];
		BallsManager.TotalBallInLevel = 0;
		for (int i = 0; i < this._normalSpawnBall.Length; i += 2)
		{
			this._normalSpawnBall[i] = this._smallBallNumber[UnityEngine.Random.Range(0, this._smallBallNumber.Length)];
		}
		for (int j = 0; j < this._normalSpawnBall.Length; j++)
		{
			if (this._normalSpawnBall[j] == 0)
			{
				if (this._currentLevel <= 3)
				{
					this._normalSpawnBall[j] = this._bigBallNumber[0];
				}
				else
				{
					this._normalSpawnBall[j] = this._bigBallNumber[UnityEngine.Random.Range(0, this._bigBallNumber.Length)];
				}
			}
			else if (this._normalSpawnBall[j] == 0)
			{
				this._normalSpawnBall[j] = this._smallBallNumber[UnityEngine.Random.Range(0, this._smallBallNumber.Length)];
			}
			BallsManager.TotalBallInLevel += this._normalSpawnBall[j];
		}
		BallsManager.CurrentBall = 0;
	}

	private IEnumerator SpawnBall()
	{
		this._t1 = this._currentDamage * (float)(this._currentBulletPerSecond - 1) * 10f / (float)this._currentLevel;
		if (this._t1 >= (float)this._currentLevel)
		{
			if (this._t1 >= 2f)
			{
				this._t2 = 1.55f;
			}
			else if (1f <= this._t1 && this._t1 < 2f)
			{
				this._t2 = UnityEngine.Random.Range(1.55f, 1.6f);
			}
		}
		else if (this._t1 <= 0.5f)
		{
			this._t2 = UnityEngine.Random.Range(1.6f, 1.62f);
		}
		else if (0.5f < this._t1 && this._t1 < 1f)
		{
			this._t2 = UnityEngine.Random.Range(1.62f, 1.65f);
		}
		if (this._currentLevel <= 5)
		{
			this._t2 = 1.2f;
		}
		else if (BallsManager.CurrentBall <= 10)
		{
			this._t2 = 1.3f;
		}
		if (this.CurrentBallIndex == 0)
		{
			yield return new WaitForSeconds(1.5f);
		}
		else
		{
			yield return new WaitForSeconds(this.waitTime);
		}
		if (this.PlayController.FrameGame.Balls.Count < this._difficultNumber && this.PlayController.GameStatus == PlayController.Game_Status.PLAYING)
		{
			float num = Mathf.Sqrt(this._currentDamage - 1f) + 0.1f;
			if (num < 0.5f)
			{
				num = 0.5f;
			}
			this.SpawnFormSpawner(this._currentLevel, (int)(this._t2 * num * 3f * Mathf.Sqrt((float)(this._currentBulletPerSecond * this._currentLevel))) + this._currentLevel * this._currentLevel / 3, this._normalSpawnBall[this.CurrentBallIndex]);
			this.CurrentBallIndex++;
		}
		if (this.CurrentBallIndex <= this._normalSpawnBall.Length - 1)
		{
			base.StartCoroutine(this.SpawnBall());
		}
		yield break;
	}

	public void SpawnFormSpawner(int min, int max, int size)
	{
		int level = 0;
		if (size != 1)
		{
			if (size != 3)
			{
				if (size != 7)
				{
					if (size == 15)
					{
						level = 3;
					}
				}
				else
				{
					level = 2;
					if (this._currentLevel == 1)
					{
						max = 2;
					}
					else if (this._currentLevel == 2)
					{
						max = 4;
					}
				}
			}
			else
			{
				level = 1;
			}
		}
		else
		{
			level = 0;
		}
		this.PlayController.FrameGame.SpawnBall(min, max, level);
	}

	private void OnDisable()
	{
		BallsManager.CurrentBall = 0;
	}

	public PlayController PlayController;

	private int _currentLevel;

	private int[] _normalSpawnBall;

	private int[] _smallBallNumber = new int[]
	{
		1,
		3
	};

	private int[] _bigBallNumber = new int[]
	{
		7,
		15
	};

	public int NumberOfBallToSpawn;

	public int CurrentBallIndex = 1;

	private float waitTime = 3.5f;

	private float _currentDamage;

	private int _currentBulletPerSecond;

	private int _difficultNumber = 8;

	private GameObject[] _smallBalls;

	private float _t1;

	private float _t2;

	public static int CurrentBall;

	public static int TotalBallInLevel;
}
