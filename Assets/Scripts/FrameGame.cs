
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FrameGame : BaseController
{
	public void ReSet ()
	{
		for (int i = 0; i < this.Coins.Count; i++) {
			UnityEngine.Object.Destroy (this.Coins [i].gameObject);
		}
		this.Coins.Clear ();
		for (int j = 0; j < this.BombBosss.Count; j++) {
			UnityEngine.Object.Destroy (this.BombBosss [j].gameObject);
		}
		this.BombBosss.Clear ();
		if (this.Boss) {
			UnityEngine.Object.Destroy (this.Boss.gameObject);
		}
	}

	private void Awake ()
	{
		this.VertExtent = Camera.main.orthographicSize;
		this.HorzExtent = this.VertExtent * (float)Screen.width / (float)Screen.height;
		this.InitFrame ();
	}

	private void Start ()
	{
	}

	public void EndBoss ()
	{
		for (int i = 0; i < this.Balls.Count; i++) {
			this.PlayController.EffectController.StartExploEffect (this.Balls [i]);
			UnityEngine.Object.Destroy (this.Balls [i].gameObject);
		}
		this.Balls.Clear ();
		for (int j = 0; j < this.BombBosss.Count; j++) {
			UnityEngine.Object.Destroy (this.BombBosss [j].gameObject);
		}
		this.BombBosss.Clear ();
		Preference.Instance.DataGame.NumBossKill++;
		this.PlayController.NextLevel ();
	}

	private void InitFrame ()
	{
		this.Ground.size = new Vector2 (this.HorzExtent * 2f, this.Ground.size.y);
		this.LeftFrame.size = new Vector2 (this.LeftFrame.size.x, this.VertExtent * 2f);
		this.RightFrame.size = new Vector2 (this.RightFrame.size.x, this.VertExtent * 2f);
		float x = -this.HorzExtent - this.LeftFrame.size.x / 2f;
		float x2 = this.HorzExtent + this.RightFrame.size.x / 2f;
		this.LeftFrame.gameObject.transform.position = new Vector2 (x, this.LeftFrame.gameObject.transform.position.y);
		this.RightFrame.gameObject.transform.position = new Vector2 (x2, this.RightFrame.gameObject.transform.position.y);
	}

	public void CreateBoss ()
	{
		int num = Preference.Instance.DataGame.CurrentLevel + 1;
		this.Boss = UnityEngine.Object.Instantiate<GameObject> (this.PlayController.BossPrefabs [(num / 5 % 2 != 1) ? 1 : 0]).GetComponent<Boss> ();
		float num2 = (float)Preference.Instance.DataGame.FireSpeed;
		float origilHearth = 14f * (Mathf.Sqrt (num2 * (float)num) * Mathf.Sqrt (Preference.Instance.DataGame.FirePower) + (float)(num * num));
		if (num == 5) {
			origilHearth = 8f * (Mathf.Sqrt (num2 * (float)num) + (float)(num * num));
		}
		Vector3 position = this.Boss.transform.position;
		this.Boss.transform.position = new Vector3 (position.x, position.y + 3f);
		this.Boss.transform.localScale = Vector3.one * this.PlayController.Canon._canonScale;
		this.Boss.SetOrigilHearth (origilHearth);
		this.Boss.transform.SetParent (base.transform, false);
		GameController.AudioController.PlayOneShot ("Audios/Effect/monster");
		this.Boss.transform.DOMoveY (position.y, 1f, false).SetEase (Ease.OutBounce).OnComplete (delegate {
			this.Boss.StartMove ();
		});
		if (num == 5) {
			this.Boss.TimeFire = 2f;
			this.Boss.TimeSpawnBall = 5f;
		} else if (num == 10) {
			this.Boss.TimeFire = 1.5f;
			this.Boss.TimeSpawnBall = 4f;
		} else if (num == 15) {
			this.Boss.TimeFire = 1.5f;
			this.Boss.TimeSpawnBall = 3.5f;
		} else {
			this.Boss.TimeFire = 1.5f;
			this.Boss.TimeSpawnBall = 3.5f;
		}
	}

	public void SpawnBallBoss (int HP, int level, Vector3 position)
	{
		Ball component = BaseController.InstantiatePrefab ("Prefabs/Components/Ball").GetComponent<Ball> ();
		this.Balls.Add (component);
		component.transform.SetParent (base.transform);
		component.InitBall (this, level, HP);
		component.StartRig ();
		component.transform.position = position;
		component.Rigidbody2D.AddForce (new Vector2 ((float)UnityEngine.Random.Range (-120, -70), (float)UnityEngine.Random.Range (150, 200)));
		Vector3 localScale = component.transform.localScale;
		component.transform.localScale = Vector3.zero;
		Color color = component.SpriteRenderer.color;
		component.SpriteRenderer.color = this.Boss.BossHit.BloodColor;
		component.SpriteRenderer.DOColor (color, 0.5f);
		component.transform.DOScale (localScale, 0.3f);
		BallsManager.CurrentBall++;
		if (this.PlayController.GameStatus != PlayController.Game_Status.PLAYING) {
			component.Pause ();
		}
	}

	public void SpawnBall (int minHP, int maxHP, int level)
	{
		Ball component = BaseController.InstantiatePrefab ("Prefabs/Components/Ball").GetComponent<Ball> ();
		component.transform.SetParent (base.transform);
		bool flag = UnityEngine.Random.Range (0, 2) == 0;
		Vector2 position = new Vector2 ((!flag) ? this.HorzExtent : (-this.HorzExtent), UnityEngine.Random.Range (this.VertExtent / 2f, this.VertExtent * 4f / 5f));
		if (!this.PlayController.SurvivalMode) {
			if (Preference.Instance.DataGame.NumLose >= 25) {
				maxHP = (int)((float)maxHP * 0.5f);
			} else if (Preference.Instance.DataGame.NumLose >= 20) {
				maxHP = (int)((float)maxHP * 0.7f);
			} else if (Preference.Instance.DataGame.NumLose >= 15) {
				maxHP = (int)((float)maxHP * 0.8f);
			} else if (Preference.Instance.DataGame.NumLose >= 10) {
				maxHP = (int)((float)maxHP * 0.9f);
			}
		}
		component.InitBall (this, position, flag, (!flag) ? (this.HorzExtent * 2f / 3f) : (-this.HorzExtent * 2f / 3f), level, UnityEngine.Random.Range (minHP, maxHP));
		component.IndexBall = this.PlayController.BallsManager.CurrentBallIndex;
		this.Balls.Add (component);
		BallsManager.CurrentBall++;
		if (this.PlayController.GameStatus != PlayController.Game_Status.PLAYING) {
			component.Pause ();
		}
	}

	public void SplitBall (Ball ball, Vector3 position)
	{
		if (ball && ball.Size > 0 && ball.CircleCollider2D.enabled) {
			Ball component = BaseController.InstantiatePrefab ("Prefabs/Components/Ball").GetComponent<Ball> ();
			component.transform.SetParent (base.transform);
			component.InitBall (this, ball.Size - 1, Mathf.RoundToInt ((float)ball.OrigilHearth / 2f));
			component.StartRig ();
			component.transform.position = position;
			if (component.transform.position.x < -this.HorzExtent + component.CircleCollider2D.radius * component.transform.localScale.x) {
				component.transform.position = new Vector2 (-this.HorzExtent + component.CircleCollider2D.radius * component.transform.localScale.x, ball.transform.position.y);
			}
			if (component.transform.position.x > this.HorzExtent - component.CircleCollider2D.radius) {
				component.transform.position = new Vector2 (this.HorzExtent - component.CircleCollider2D.radius * component.transform.localScale.x, ball.transform.position.y);
			}
			component.Rigidbody2D.AddForce (new Vector2 ((float)UnityEngine.Random.Range (-120, -70), (float)UnityEngine.Random.Range (150, 200)));
			this.Balls.Add (component);
			if (this.PlayController.GameStatus != PlayController.Game_Status.PLAYING) {
				component.Pause ();
			}
			Ball component2 = BaseController.InstantiatePrefab ("Prefabs/Components/Ball").GetComponent<Ball> ();
			component2.transform.SetParent (base.transform);
			component2.InitBall (this, ball.Size - 1, Mathf.RoundToInt ((float)ball.OrigilHearth / 2f));
			component2.StartRig ();
			component2.transform.position = position;
			if (component2.transform.position.x < -this.HorzExtent + component2.CircleCollider2D.radius * component2.transform.localScale.x) {
				component2.transform.position = new Vector2 (-this.HorzExtent + component2.CircleCollider2D.radius * component2.transform.localScale.x, component2.transform.position.y);
			}
			if (component2.transform.position.x > this.HorzExtent - component2.CircleCollider2D.radius) {
				component2.transform.position = new Vector2 (this.HorzExtent - component2.CircleCollider2D.radius * component2.transform.localScale.x, component2.transform.position.y);
			}
			component2.Rigidbody2D.AddForce (new Vector2 ((float)UnityEngine.Random.Range (70, 120), (float)UnityEngine.Random.Range (150, 200)));
			this.Balls.Add (component2);
			if (this.PlayController.GameStatus != PlayController.Game_Status.PLAYING) {
				component2.Pause ();
			}
			BallsManager.CurrentBall += 2;
		} else if (ball && ball.Size == 0) {
			this.SpawnCoin (ball.transform.position);
		}
		if (!this.PlayController.SurvivalMode) {
			if (!this.PlayController.BossFighting) {
				this.PlayController.PlayUI.UpdateProgress (BallsManager.CurrentBall);
			}
		} else {
			this.PlayController.PlayUI.AddScore (2);
		}
		this.PlayController.EffectController.StartExploEffect (ball);
		ball.CircleCollider2D.enabled = false;
		UnityEngine.Object.Destroy (ball.gameObject);
		this.Balls.Remove (ball);
		Preference.Instance.DataGame.NumBallShoot++;
		GameController.AudioController.PlayVibrate ();
		GameController.AudioController.PlayOneShot ("Audios/Effect/ball_break_01");
		if (!this.PlayController.SurvivalMode && !this.PlayController.BossFighting && this.PlayController.BallsManager.CurrentBallIndex >= this.PlayController.BallsManager.NumberOfBallToSpawn && this.Balls.Count == 0) {
			this.PlayController.NextLevel ();
		
			//GooglePlayServiceManager.Instance.ReportScore (GPGSIds.leaderboard_challenge_high_level, Preference.Instance.DataGame.CurrentLevel++);
		}
	}

	public void SpawnCoin (Vector3 position)
	{
		Coin component = BaseController.InstantiatePrefab ("Prefabs/Components/Coin").GetComponent<Coin> ();
		component.transform.SetParent (base.transform);
		this.Coins.Add (component);
		component.InitCoin (Preference.Instance.DataGame.CoinDrop, position, this.HorzExtent / 18f, this);
	}

	private void Update ()
	{
	}

	public BoxCollider2D Ground;

	public BoxCollider2D LeftFrame;

	public BoxCollider2D RightFrame;

	public float VertExtent;

	public float HorzExtent;

	public List<Ball> Balls = new List<Ball> ();

	public List<Coin> Coins = new List<Coin> ();

	public Boss Boss;

	public List<BombBoss> BombBosss = new List<BombBoss> ();

	public PlayController PlayController;
}
