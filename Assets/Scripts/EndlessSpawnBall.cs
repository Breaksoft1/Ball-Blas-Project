
using System;
using System.Collections;
using UnityEngine;

public class EndlessSpawnBall : MonoBehaviour
{
	public void Spawn()
	{
		base.StopAllCoroutines();
		this.Number = 1.5f;
		this._difficultNumber = 8;
		base.StartCoroutine(this.BallSpawner());
		base.StartCoroutine(this.DiffcultyUp());
		this.SpawnBallFromSpawner(Preference.Instance.DataGame.FirePower, Preference.Instance.DataGame.FirePower * 1.5f);
	}

	public void SpawnBallFromSpawner(float min, float max)
	{
		this.PlayController.FrameGame.SpawnBall((int)min, (int)max, UnityEngine.Random.Range(0, 4));
	}

	private IEnumerator BallSpawner()
	{
		yield return new WaitForSeconds(2.5f);
		if (this.PlayController.FrameGame.Balls.Count <= this._difficultNumber && this.PlayController.GameStatus == PlayController.Game_Status.PLAYING)
		{
			this.SpawnBallFromSpawner(Preference.Instance.DataGame.FirePower * 2f, Preference.Instance.DataGame.FirePower * (float)Preference.Instance.DataGame.FireSpeed * this.Number);
		}
		base.StartCoroutine(this.BallSpawner());
		yield break;
	}

	private IEnumerator DiffcultyUp()
	{
		yield return new WaitForSeconds(10f);
		float va = Preference.Instance.DataGame.FirePower * (float)Preference.Instance.DataGame.FireSpeed;
		if (va <= 10f)
		{
			this.Number += 0.4f;
		}
		else if (va <= 30f)
		{
			this.Number += 0.3f;
		}
		else if (va <= 80f)
		{
			this.Number += 0.3f;
		}
		else if (va <= 200f)
		{
			this.Number += 0.2f;
		}
		else
		{
			this.Number += 0.1f;
		}
		base.StartCoroutine(this.DiffcultyUp());
		yield break;
	}

	public PlayController PlayController;

	private int _difficultNumber;

	public float Number;
}
