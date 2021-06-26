
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
	public void OpenStage(ScreenManager.StateGame stateGame)
	{
		this.OpenStage(stateGame, null);
	}

	public void OpenStage(ScreenManager.StateGame stateGame, ScreenManager.CallBack callBack)
	{
		ScreenManager.isLoadScreen = true;
		base.StopAllCoroutines();
		base.StartCoroutine(this._OpenStage(stateGame, callBack));
	}

	public IEnumerator _OpenStage(ScreenManager.StateGame _stateGame, ScreenManager.CallBack callBack)
	{
		if (this.currentStage != null)
		{
			this.currentStage.OnStageClose();
		}
		this.backState = this.stateGame;
		this.stateGame = _stateGame;
		string nameScene = "Play";
		if (_stateGame != ScreenManager.StateGame.PLAY)
		{
			if (_stateGame == ScreenManager.StateGame.MAIN)
			{
				nameScene = "Main";
			}
		}
		else
		{
			nameScene = "Play";
		}
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nameScene);
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		yield return null;
		if (_stateGame != ScreenManager.StateGame.PLAY)
		{
			if (_stateGame == ScreenManager.StateGame.MAIN)
			{
				this.MainController = UnityEngine.Object.FindObjectOfType<MainController>();
				this.currentStage = this.MainController;
			}
		}
		else
		{
			this.PlayController = UnityEngine.Object.FindObjectOfType<PlayController>();
			this.currentStage = this.PlayController;
		}
		if (callBack != null)
		{
			callBack();
		}
		if (this.stateGame == _stateGame)
		{
			ScreenManager.isLoadScreen = false;
			if (this.currentStage != null)
			{
				this.currentStage.OnStageOpen();
			}
		}
		yield break;
	}

	[HideInInspector]
	public static bool isLoadScreen;

	[HideInInspector]
	public LoadController LoadController;

	[HideInInspector]
	public PlayController PlayController;

	[HideInInspector]
	public MainController MainController;

	[HideInInspector]
	public StageController currentStage;

	[HideInInspector]
	public ScreenManager.StateGame stateGame;

	[HideInInspector]
	public ScreenManager.StateGame backState;

	[HideInInspector]
	public delegate void CallBack();

	public enum StateGame
	{
		LOAD,
		MAIN,
		PLAY
	}
}
