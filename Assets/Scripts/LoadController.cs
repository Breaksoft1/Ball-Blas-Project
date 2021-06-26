
using System;
using System.Collections;
using UnityEngine;

public class LoadController : StageController
{
	private void Start()
	{
		base.StartCoroutine(this.StartGame());
	}

	private IEnumerator StartGame()
	{
		//yield return new WaitForSeconds(0.5f);
			GameController.ScreenManager.OpenStage(ScreenManager.StateGame.PLAY);
		yield break;
	}

	private void Update()
	{
	}
}
