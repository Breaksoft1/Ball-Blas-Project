
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
//using Firebase.Analytics;
//using Facebook.Unity;

public class AnalyticsController : BaseController
{
	public void Start ()
	{
		

//		Firebase.FirebaseApp.CheckAndFixDependenciesAsync ().ContinueWith (task => {
//			var dependencyStatus = task.Result;
//			if (dependencyStatus == Firebase.DependencyStatus.Available) {
//				// Create and hold a reference to your FirebaseApp,
//				// where app is a Firebase.FirebaseApp property of your application class.
//				//   app = Firebase.FirebaseApp.DefaultInstance;

//				// Set a flag here to indicate whether Firebase is ready to use by your app.
//			} else {
//				UnityEngine.Debug.LogError (System.String.Format (
//					"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//				// Firebase Unity SDK is not safe to use here.
//			}
//		});

//		try {
//			if (!FB.IsInitialized) {
////				if (AnalyticsController._003C_003Ef__mg_0024cache0 == null) {
////					AnalyticsController._003C_003Ef__mg_0024cache0 = new InitDelegate (FB.ActivateApp);
////				}
		//		FB.Init (null, null, null);
		//	} else {
		//		FB.ActivateApp ();
		//	}
		//	FirebaseAnalytics.LogEvent (FirebaseAnalytics.EventLogin);
		//} catch (Exception message) {
		//	UnityEngine.Debug.Log (message);
		//}
	}

	public void LogEvent (string eventName)
	{
		//try {
		//	FirebaseAnalytics.LogEvent (eventName);
		//	this.FBLogEvent (eventName, null, 0f);
		//} catch (Exception message) {
		//	UnityEngine.Debug.Log (message);
		//}
	}

	public void LogEventPurchase (string productID, int value)
	{
		//FirebaseAnalytics.LogEvent("ChargeIAP", new Parameter[]
		//{
		//	new Parameter("value", (long)value),
		//	new Parameter("productID", productID)
		//});
		//Dictionary<string, object> dictionary = new Dictionary<string, object>();
		//dictionary["productID"] = productID;
		//FB.LogPurchase((float)value, "USD", dictionary);
	}

	public void LogEvent (string eventName, string paramName, float paramValue)
	{
		//try {
		//	FirebaseAnalytics.LogEvent (eventName, paramName, (double)paramValue);
		//	this.FBLogEvent (eventName, paramName, paramValue);
		//} catch (Exception message) {
		//	UnityEngine.Debug.Log (message);
		//}
	}

	public void LogEvent (string eventName, string paramName, string paramValue)
	{
		//try {
		//	FirebaseAnalytics.LogEvent (eventName, paramName, paramValue);
		//	this.FBLogEvent (eventName + "_" + paramValue, paramName, 0f);
		//	this.FBLogEvent (eventName, null, 0f);
		//} catch (Exception message) {
		//	UnityEngine.Debug.Log (message);
		//}
	}

	public void FBLogEvent (string eventName, string paramName, float paramValue)
	{
		//Dictionary<string, object> parameters = null;
		//if (paramName != null) {
		//	parameters = new Dictionary<string, object> ();
		//	parameters [paramName] = paramName;
		//}
		//if (!FB.IsInitialized) {
		//	FB.Init (delegate() {
		//		FB.LogAppEvent (eventName, new float? (paramValue), parameters);
		//	}, null, null);
		//} else {
		//	FB.LogAppEvent (eventName, new float? (paramValue), parameters);
		//}
	}

	public static string START_TUTORIAL_STEP = "Start_tutorial";

	public static string STEP = "Step";

	public static string WATCH_ADS = "Watch_ads";

	public static string WATCH_ADS_TYPE = "Type";

	public static string GAME_OVER = "Game_over";

	public static string SCORE = "Score";

	public static string PURCHASE_REMOVE_ADS = "Purchase_remove_ads";

	public static string PURCHASE = "Purchase";

	public static string PACKAGE = "Package";

	public static string USE_THEME = "Use_theme";

	public static string NAME = "Name";

	public static string DAILY_REWARD = "Daily_Reward";

	public static string DAY = "Day";

	public static string FINISH_TUTORIAL = "Finish_tutorial";

	public static string SPIN = "Spin";

	public static string Gem = "Gem";

	public static string Gem2 = "GemX2";

	public static string START_GAME = "Start_game";

	public static string USE_BIN = "Use_bin";

	//	[CompilerGenerated]
	//private static InitDelegate _003C_003Ef__mg_0024cache0;
}
