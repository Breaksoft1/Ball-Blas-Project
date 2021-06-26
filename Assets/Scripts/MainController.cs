
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainController : StageController
{
	private void Start()
	{
		this.ButtonPlay.onClick.AddListener(delegate()
		{
			this.Play();
		});
	}

	private void Update()
	{
	}

	private void Play()
	{
		GameController.ScreenManager.OpenStage(ScreenManager.StateGame.PLAY);
		base.OnStageOpen();
	}

	public override void OnStageOpen()
	{
		base.OnStageOpen();
        //GameController.AdsController.SetBannerShow(false);
    }

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{

        }
	}

	public Button ButtonPlay;

	public RectTransform RectTransform;
}
