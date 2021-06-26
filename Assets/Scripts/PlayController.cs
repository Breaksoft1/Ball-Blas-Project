
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayController : StageController
{
	private void Start ()
	{
		this.SetBackground (UnityEngine.Random.Range (0, this.BackgroundPrefabs.Length));
		this.SetCanon (Preference.Instance.DataGame.CurrenCanon);
		TuNDPool.Preload (this.Canon.Bullet, this.FrameGame.transform, 20);
		this.Tutorial = false;
		this.CountPlay = 0;
	}

	private void Update ()
	{
		if (this.GameStatus == PlayController.Game_Status.PLAYING) {
			this._timePlay += Time.deltaTime;
		}
	}

	public void CheckSpriteBullet ()
	{
		if (this._backgroundId == 1 || this._backgroundId == 5 || this._backgroundId == 6) {
			this.BulletSprite = this.BulletSprites [UnityEngine.Random.Range (1, 5)];
			if (Preference.Instance.DataGame.CurrenCanon == 7) {
				this.BulletSprite = this.BulletSprites [6];
			}
			if (Preference.Instance.DataGame.CurrenCanon == 8) {
				this.BulletSprite = this.BulletSprites [8];
			}
			if (Preference.Instance.DataGame.CurrenCanon == 9) {
				this.BulletSprite = this.BulletSprites [10];
			}
		} else {
			this.BulletSprite = this.BulletSprites [UnityEngine.Random.Range (0, 3)];
			if (Preference.Instance.DataGame.CurrenCanon == 7) {
				this.BulletSprite = this.BulletSprites [5];
			}
			if (Preference.Instance.DataGame.CurrenCanon == 8) {
				this.BulletSprite = this.BulletSprites [7];
			}
			if (Preference.Instance.DataGame.CurrenCanon == 9) {
				this.BulletSprite = this.BulletSprites [9];
			}
		}
	}

	public void SetBackground (int id)
	{
		this._backgroundId = id;
		if (id == 1 || id == 5 || id == 6) {
			this.MenuUI.TextSwipeToPlay.color = Color.white;
			this.BulletSprite = this.BulletSprites [UnityEngine.Random.Range (1, 5)];
		} else {
			this.MenuUI.TextSwipeToPlay.color = new Color (0.3764706f, 0.3764706f, 0.3764706f);
			this.BulletSprite = this.BulletSprites [UnityEngine.Random.Range (0, 3)];
		}
		if (id == 3) {
			this.PlayUI.TextScore.color = new Color (0.435294122f, 0.435294122f, 0.435294122f);
			this.BulletSprite = this.BulletSprites [UnityEngine.Random.Range (0, 3)];
		} else {
			this.PlayUI.TextScore.color = Color.white;
		}
		if (this.Background) {
			UnityEngine.Object.Destroy (this.Background);
		}
		this.Background = UnityEngine.Object.Instantiate<GameObject> (this.BackgroundPrefabs [id], this.BackgroundParent);
		this.Background.transform.localPosition = Vector2.zero;
		this.Background.transform.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
		Camera.main.transform.position = new Vector3 (0f, 0f, -10f);
		this.CheckSpriteBullet ();
	}

	public void SetCanon (int id)
	{
		Preference.Instance.DataGame.CurrenCanon = id;
		if (this.Canon) {
			UnityEngine.Object.Destroy (this.Canon.gameObject);
		}
		this.Canon = UnityEngine.Object.Instantiate<GameObject> (this.CanonPrefabs [id], this.CanonParent).GetComponent<Canon> ();
		this.Canon.transform.localPosition = new Vector2 (0f, 3f);
		this.Canon.transform.DOLocalMoveY (0f, 0.5f, false).SetEase (Ease.OutBounce).SetDelay (0.05f);
		this.CheckSpriteBullet ();
	}

	public void StartGame ()
	{
		this.CountPlay++;
		int num = UnityEngine.Random.Range (1, 5);
		this.BallSprite = Resources.Load<Sprite> ("Images/Gameplay/Ball/ball_" + num);
		this.SetGameStatus (PlayController.Game_Status.PLAYING);
		this.PlayUI.SetLevel (Preference.Instance.DataGame.CurrentLevel);
		this._secondChance = false;
		this.Canon.Reset ();
		this.FrameGame.ReSet ();
		this.CurrentCoin = 0;
		this._timePlay = 0f;
		if (!Preference.Instance.DataGame.CannonStatuses [this.Canon.CanonId].IsOpen) {
			Preference.Instance.DataGame.CannonStatuses [this.Canon.CanonId].NumTry--;
			if (Preference.Instance.DataGame.CannonStatuses [this.Canon.CanonId].NumTry < 0) {
				Preference.Instance.DataGame.CannonStatuses [this.Canon.CanonId].NumTry = 0;
			}
		}
		Preference.Instance.DataGame.NumPlay++;
		GameController.AudioController.PlayLoop ((UnityEngine.Random.Range (0, 2) != 1) ? "Audios/background2" : "Audios/background");
		if (!this.SurvivalMode) {
			this.BossFighting = ((Preference.Instance.DataGame.CurrentLevel + 1) % 5 == 0);
			if (!this.BossFighting) {
				this.BallsManager.Spawn ();
			} else {
				this.FrameGame.CreateBoss ();
			}
			this.SetLevelStat (BallsManager.TotalBallInLevel);
			GameController.AnalyticsController.LogEvent ("start_game", "level", (float)Preference.Instance.DataGame.CurrentLevel);
		} else {
			this.EndlessSpawnBall.Spawn ();
			GameController.AnalyticsController.LogEvent ("start_game_surviral");
		}
	}

	public void CheckAvaiableCanon ()
	{
		if (!Preference.Instance.DataGame.CannonStatuses [this.Canon.CanonId].IsOpen && Preference.Instance.DataGame.CannonStatuses [this.Canon.CanonId].NumTry <= 0) {
			this.SetCanon (0);
		}
	}

	public override void OnStageClose ()
	{
		this.SavePlayData ();
		base.OnStageClose ();
	}

	public override void OnStageOpen ()
	{
		base.OnStageOpen ();
		this.SetGameStatus (PlayController.Game_Status.START);
		this.CheckOfflineEarn ();
	}

	public void SetLevelStat (int maxBall)
	{
		this.PlayUI.SetMaxBall (maxBall);
	}

	public void NextLevel ()
	{

		//GooglePlayServiceManager.Instance.ReportScore (GPGSIds.leaderboard_challenge_high_level, (Preference.Instance.DataGame.CurrentLevel + 1));

		this.GameStatus = PlayController.Game_Status.WAIT;
		this.PlayUI.SetPercent (100);
		base.StartCoroutine (this.IeNextLevel ());
	}

	private IEnumerator IeNextLevel ()
	{
		yield return new WaitForSeconds ((!this.BossFighting) ? 1.5f : 4f);
		this.GameStatus = PlayController.Game_Status.GAMEOVER;
		GameController.AudioController.PlayOneShot ("Audios/Effect/reward");
		GameController.AnalyticsController.LogEvent ("win_game", "time_play", this._timePlay);
		if (Preference.Instance.DataGame.CurrentLevel == 0) {
			GameController.AnalyticsController.LogEvent ("win_game_level_1");
		} else if (Preference.Instance.DataGame.CurrentLevel == 2) {
			GameController.AnalyticsController.LogEvent ("win_game_level_3");
		} else if (Preference.Instance.DataGame.CurrentLevel == 4) {
			GameController.AnalyticsController.LogEvent ("win_game_level_5");
		} else if (Preference.Instance.DataGame.CurrentLevel == 9) {
			GameController.AnalyticsController.LogEvent ("win_game_level_10");
		}
		Preference.Instance.DataGame.HighPercent = 0;
		Preference.Instance.DataGame.NumLose = 0;
		Preference.Instance.DataGame.CurrentLevel++;
		GameController.DialogManager.DialogLevelClear.Show ();


		this.Canon.Reset ();
		this.FrameGame.ReSet ();
		yield break;
	}

	public void GameOver ()
	{
        this.GameStatus = PlayController.Game_Status.GAMEOVER;
        if (!this._secondChance
            && Preference.Instance.DataGame.CurrentLevel >= 2
            && Preference.Instance.DataGame.FirstDie
            && PlayUI.Score >= 50)
        {
            this._secondChance = true;
            this.PauseGame();
            GameController.DialogManager.DialogSecondChance.Show();
        }
        else {
            if (!Preference.Instance.DataGame.FirstDie)
            {
                Preference.Instance.DataGame.FirstDie = true;
                this.Tutorial = true;
            }
            this.PauseGame();
            if (this._currentGameState != null)
            {
                this._currentGameState.Close();
            }
            if (!this.SurvivalMode)
            {
                GameController.DialogManager.DialogGameOver.Show();
                Preference.Instance.DataGame.NumLose++;
                GameController.AnalyticsController.LogEvent("lose_game", "time_play", this._timePlay);
                if (Preference.Instance.DataGame.NumLose >= 25)
                {
                    GameController.AnalyticsController.LogEvent("lose_game_25");
                }
                else if (Preference.Instance.DataGame.NumLose >= 20)
                {
                    GameController.AnalyticsController.LogEvent("lose_game_20");
                }
                else if (Preference.Instance.DataGame.NumLose >= 15)
                {
                    GameController.AnalyticsController.LogEvent("lose_game_15");
                }
                else if (Preference.Instance.DataGame.NumLose >= 10)
                {
                    GameController.AnalyticsController.LogEvent("lose_game_10");
                }
            }
            else {
                GameController.DialogManager.DialogGameOver.Show();
                GameController.AnalyticsController.LogEvent("lose_game_survival", "score", (float)this.PlayUI.Score);
                //GooglePlayServiceManager.Instance.ReportScore(GPGSIds.leaderboard_survival_high_score, this.PlayUI.Score);
            }
        }
    }

	public void PauseGame ()
	{
		for (int i = 0; i < this.FrameGame.Balls.Count; i++) {
			this.FrameGame.Balls [i].Pause ();
		}
	}

	public void ResumeGame ()
	{
		for (int i = 0; i < this.FrameGame.Balls.Count; i++) {
			this.FrameGame.Balls [i].Resume ();
		}
	}

	public void SecondChance ()
	{
		this.GameStatus = PlayController.Game_Status.PLAYING;
		this.Canon.TimeImmortal = 3f;
		this.ResumeGame ();
	}

	public void ContinueGameOver ()
	{
		this.BallsManager.StopAllCoroutines ();
		this.EndlessSpawnBall.StopAllCoroutines ();
		for (int i = 0; i < this.FrameGame.Balls.Count; i++) {
			this.FrameGame.Balls [i].Pause ();
		}
		for (int j = 0; j < this.FrameGame.Balls.Count; j++) {
			this.FrameGame.Balls [j].DestroyBall ();
		}
		this.SetBackground (UnityEngine.Random.Range (0, this.BackgroundPrefabs.Length));
		this.FrameGame.Balls.Clear ();
		this.FrameGame.ReSet ();
		this.SetGameStatus (PlayController.Game_Status.START);
		this.Canon.Reset ();
	}

	private void OnApplicationPause (bool pauseStatus)
	{
		this.SavePlayData ();
		if (!pauseStatus) {
			this.CheckOfflineEarn ();
		}
	}

	private void CheckOfflineEarn ()
	{
		if (this.GameStatus != PlayController.Game_Status.PLAYING) {
			if (Preference.Instance.DataGame.LastOnlineTime != 0L && TimeSpan.FromTicks (DateTime.Now.Ticks - Preference.Instance.DataGame.LastOnlineTime).TotalSeconds > 600.0) {
				GameController.DialogManager.DialogIdleEarn.Show ();
			}
			if (this.DailyRewardAvailable) {
				GameController.DialogManager.DialogDailyReward.Show (Preference.Instance.DataGame.DailyRewardCycleCount);
				Preference.Instance.DataGame.ResetDailyReward ();
				this.MenuUI.UpdateMission ();
			}
		}
	}

	private void OnApplicationQuit ()
	{
		this.SavePlayData ();
	}

	public void SavePlayData ()
	{
		Preference.Instance.SaveData ();
	}

	public void SetGameStatus (PlayController.Game_Status status)
	{
		this.GameStatus = status;
		if (this._currentGameState != null) {
			this._currentGameState.Close ();
		}
		PlayController.Game_Status gameStatus = this.GameStatus;
		if (gameStatus != PlayController.Game_Status.START) {
			if (gameStatus != PlayController.Game_Status.PLAYING) {
				this._currentGameState = this.MenuUI;
			} else {
				//GameController.AdsController.SetBannerShow (true);
				this._currentGameState = this.PlayUI;
			}
		} else {
			//GameController.AdsController.SetBannerShow (false);
			this.CheckAvaiableCanon ();
			GameController.AudioController.StopBackgroundMusic ("Audios/background");
			GameController.AudioController.StopBackgroundMusic ("Audios/background2");
			this._currentGameState = this.MenuUI;
		}
		this._currentGameState.Open ();
	}

	public bool DailyRewardAvailable {
		get {
			bool result;
			if (Preference.Instance.DataGame.DailyRewardLastTime != 0L) {
				DateTime dateTime = new DateTime (Preference.Instance.DataGame.DailyRewardLastTime);
				result = (dateTime.Date.Ticks != DateTime.Now.Date.Ticks);
			} else {
				result = true;
			}
			return result;
		}
	}

	public void OnPurchaseCoin (int coin)
	{
		GameController.DialogManager.DialogShop.Hide ();
		GameController.DialogManager.Toast.Show ("Purchase Success!");
		GameController.AudioController.PlayOneShot ("Audios/Effect/success");
		this.MenuUI.StartAddCoinEffect (coin);
	}

	public void OnPurchaseCanon (int id)
	{
		GameController.DialogManager.DialogShopCanon.Hide ();
		Preference.Instance.DataGame.CannonStatuses [id].IsOpen = true;
		this.SetCanon (id);
		GameController.DialogManager.Toast.Show ("Purchase Success!");
		GameController.AudioController.PlayOneShot ("Audios/Effect/success");
	}

	public void OnPurchaseOffer ()
	{
		GameController.DialogManager.DialogOffer.Hide ();
		this.MenuUI.ButtonOffer.gameObject.SetActive (false);
		this.MenuUI.StartAddCoinEffect (10000);
		Preference.Instance.DataGame.CannonStatuses [3].IsOpen = true;
		Preference.Instance.DataGame.CannonStatuses [6].IsOpen = true;
		GameController.DialogManager.Toast.Show ("Purchase Success!");
		GameController.AudioController.PlayOneShot ("Audios/Effect/success");
		this.SetCanon (3);
	}

	public void OnPurchaseCombo (int id)
	{
		int coin = 200000;
		switch (id) {
		case 1:
			coin = 600000;
			for (int i = 0; i < Preference.Instance.DataGame.CannonStatuses.Length; i++) {
				Preference.Instance.DataGame.CannonStatuses [i].IsOpen = true;
			}
			this.SetCanon (3);
			break;
		case 2:
			Preference.Instance.DataGame.CannonStatuses [3].IsOpen = true;
			this.SetCanon (3);
			break;
		case 3:
			Preference.Instance.DataGame.CannonStatuses [4].IsOpen = true;
			this.SetCanon (4);
			break;
		case 4:
			Preference.Instance.DataGame.CannonStatuses [5].IsOpen = true;
			this.SetCanon (5);
			break;
		case 5:
			Preference.Instance.DataGame.CannonStatuses [6].IsOpen = true;
			this.SetCanon (6);
			break;
		}
		GameController.DialogManager.DialogShop.Hide ();
		this.MenuUI.ButtonOffer.gameObject.SetActive (false);
		this.MenuUI.StartAddCoinEffect (coin);
		GameController.DialogManager.Toast.Show ("Purchase Success!");
		GameController.AudioController.PlayOneShot ("Audios/Effect/success");
	}

	public FrameGame FrameGame;

	public BallsManager BallsManager;

	public EndlessSpawnBall EndlessSpawnBall;

	public Transform CanonParent;

	public Transform BackgroundParent;

	public GameObject Shield;

	public GameObject Background;

	[HideInInspector]
	public Canon Canon;

	public EffectController EffectController;

	public PlayUI PlayUI;

	public MenuUI MenuUI;

	private GameState _currentGameState;

	public PlayController.Game_Status GameStatus;

	private bool _secondChance;

	public int CurrentCoin;

	public GameObject[] CanonPrefabs;

	public GameObject[] BackgroundPrefabs;

	public GameObject[] BossPrefabs;

	public Sprite[] BulletSprites;

	[HideInInspector]
	public Sprite BallSprite;

	[HideInInspector]
	public Sprite BulletSprite;

	public bool BossFighting;

	private float _timePlay;

	public int CountPlay;

	public bool Tutorial;

	public bool SurvivalMode;

	private int _backgroundId;

	public enum Game_Status
	{
		PLAYING,
		PAUSE,
		START,
		GAMEOVER,
		WAIT
	}
}
