
using System;
using UnityEngine;

public class GameController : BaseController
{
	private void Awake ()
	{
		Application.targetFrameRate = 60;
		this.CanvasScale = GameController.SCREEN_GRAPHIC_HEIGHT / (float)Screen.height;
		GameController.DialogManager = base.GetComponentInChildren<DialogManager> (true);
		GameController.ScreenManager = base.GetComponentInChildren<ScreenManager> (true);
		GameController.AudioController = base.GetComponentInChildren<AudioController> (true);
		//GameController.AdsController = base.GetComponentInChildren<AdsController> (true);
		GameController.PurchaseController = base.GetComponentInChildren<PurchaseController> (true);
		GameController.AnalyticsController = base.GetComponentInChildren<AnalyticsController> (true);
	}

	public static GameController Instance {
		get {
			return BaseController.GameController;
		}
	}

    public object AdsController { get; internal set; }

    public void OnApplicationQuit ()
	{
		Preference.Instance.DataGame.LastOnlineTime = DateTime.Now.Ticks;
		Preference.Instance.SaveData ();
	}

	public void OnApplicationPause (bool pause)
	{
		if (pause) {
			Preference.Instance.DataGame.LastOnlineTime = DateTime.Now.Ticks;
		}
		Preference.Instance.SaveData ();
	}

	private void Start ()
	{
		Screen.sleepTimeout = -1;
		UnityEngine.Object.DontDestroyOnLoad (this);
	}

	public string LinkGame ()
	{
#if UNITY_ANDROID
		return "https://play.google.com/store/apps/details?id=" + Application.identifier;
#elif UNITY_IOS 
		return "itms-apps://itunes.apple.com/app/id1194904603";
#endif
        return "";
	}

	public string LinkStore ()
	{
#if UNITY_ANDROID
		return "https://play.google.com/store/apps/developer?id=SonDH";
#elif UNITY_IOS
		return "itms-apps://itunes.apple.com/developer/dung-nguyen/id1188166020";
#endif
        return "";
	}

	[HideInInspector]
	public static float SCREEN_GRAPHIC_WIDTH = 720f;

	[HideInInspector]
	public static float SCREEN_GRAPHIC_HEIGHT = 1280f;

	[HideInInspector]
	public static DialogManager DialogManager;

	[HideInInspector]
	public static ScreenManager ScreenManager;

	[HideInInspector]
	public static AudioController AudioController;

    //[HideInInspector]
    //public static AdsController AdsController;

    [HideInInspector]
	public static PurchaseController PurchaseController;

	[HideInInspector]
	public static AnalyticsController AnalyticsController;

	[HideInInspector]
	public float CanvasScale;

	[HideInInspector]
	public float HexaScale = 1f;
}
