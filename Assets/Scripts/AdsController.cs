//using System;
//using System.Collections;
//using GoogleMobileAds.Api;
//using UnityEngine;
//using UnityEngine.Advertisements;

//public class AdsController : MonoBehaviour
//{
//    private void Start()
//    {
//        this.testNoAdAvailable = false;
//#if UNITY_ANDROID
//        string appId = "ca-app-pub-6442067588290070~4319800728";

//#elif UNITY_IPHONE
//        string appId = "ca-app-pub-7682150448917230~5794882198";
//#else
//        string appId = "unexpected_platform";
//#endif
//        MobileAds.Initialize(appId);
//        this.RequestInterstitial();
//        MobileAds.SetiOSAppPauseOnBackground(true);
//        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
//        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
//        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
//        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
//        this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
//        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
//        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
//        this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
//        this.RequestRewardAdmob();
//        if (!Preference.Instance.DataGame.NoAds)
//        {
//            this.RequestBanner();
//        }
//    }

//    private void Update()
//    {
//    }

//    //public bool IsShow
//    //{
//    //    get
//    //    {
//    //        return Advertisement.isShowing;
//    //    }
//    //}

//    public void ShowReward(AdsController.AdCallBack callback)
//    {
//        this.adCallBack = callback;
//        if (UnityEngine.Random.Range(0, 2) == 1)
//        {
//            if (this.rewardBasedVideo.IsLoaded())
//            {
//                this.IsShowInter = true;
//                this.rewardBasedVideo.Show();
//                GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Reward_Admob");
//            }
//            //if (Advertisement.IsReady(this.placementId))
//            //{
//            //    //ShowOptions showOptions = new ShowOptions();
//            //    //showOptions.resultCallback = new Action<ShowResult>(this.HandleShowResult);
//            //    //this.IsShowInter = true;
//            //    //Advertisement.Show(this.placementId, showOptions);
//            //    //GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Reward_Unity");
//            //}
           
//        }
//        else if (this.rewardBasedVideo.IsLoaded())
//        {
//            this.IsShowInter = true;
//            this.rewardBasedVideo.Show();
//            GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Reward_Admob");
//        }
//        //else if (Advertisement.IsReady(this.placementId))
//        //{
//        //    this.RequestRewardAdmob();
//        //    ShowOptions showOptions2 = new ShowOptions();
//        //    showOptions2.resultCallback = new Action<ShowResult>(this.HandleShowResult);
//        //    this.IsShowInter = true;
//        //    Advertisement.Show(this.placementId, showOptions2);
//        //    GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Reward_Unity");
//        //}
//    }

//    //private void HandleShowResult(ShowResult result)
//    //{
//    //    //if (result == ShowResult.Finished)
//    //    //{
//    //    //    UnityEngine.Debug.Log("Video completed - Offer a reward to the player");
//    //    //    base.StartCoroutine(this.StartReward());
//    //    //}
//    //    //else if (result == ShowResult.Skipped)
//    //    //{
//    //    //    UnityEngine.Debug.LogWarning("Video was skipped - Do NOT reward the player");
//    //    //}
//    //    //else if (result == ShowResult.Failed)
//    //    //{
//    //    //    UnityEngine.Debug.LogError("Video failed to show");
//    //    //}
//    //}

//    //public bool IsReady()
//    //{
//    //    return !this.testNoAdAvailable && (Advertisement.IsReady(this.placementId) || (this.rewardBasedVideo != null && this.rewardBasedVideo.IsLoaded()));
//    //}

//    public void RequestBanner()
//    {
//        this.DestroyBanner();
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-6442067588290070/1897829122";

//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-7682150448917230/9425816076";
//#else
//        string adUnitId = "unexpected_platform";
//#endif
//        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
//        AdRequest request = new AdRequest.Builder().Build();
//        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
//        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
//        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
//        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
//        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
//        this.bannerView.LoadAd(request);
//    }

//    public void DestroyBanner()
//    {
//        if (this.bannerView != null)
//        {
//            this.bannerView.Destroy();
//        }
//        this.IsShowBanner = false;
//        this.IsLoadBanner = false;
//    }

//    public void SetBannerShow(bool isShow)
//    {
//        this.IsShowBanner = isShow;
//        if (this.bannerView != null)
//        {
//            if (isShow)
//            {
//                this.bannerView.Show();
//            }
//            else {
//                this.bannerView.Hide();
//            }
//        }
//    }

//    private void HandleOnAdLoaded(object sender, EventArgs args)
//    {
//        this.bannerView.Hide();
//        this.IsLoadBanner = true;
//        UnityEngine.Debug.Log("HandleAdLoaded event received");
//    }

//    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
//    }

//    private void HandleOnAdOpened(object sender, EventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleAdOpened event received");
//    }

//    private void HandleOnAdClosed(object sender, EventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleAdClosed event received");
//    }

//    private void HandleOnAdLeavingApplication(object sender, EventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleAdLeavingApplication event received");
//    }

//    private void RequestInterstitial()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-6442067588290070/6687831814";

//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-7682150448917230/1168584934";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        this.interstitial = new InterstitialAd(adUnitId);
//        this.interstitial.OnAdLoaded += this.HandleOnAdLoadedFull;
//        this.interstitial.OnAdFailedToLoad += this.HandleOnAdFailedToLoadFull;
//        this.interstitial.OnAdOpening += this.HandleOnAdOpenedFull;
//        this.interstitial.OnAdClosed += this.HandleOnAdClosedFull;
//        this.interstitial.OnAdLeavingApplication += this.HandleOnAdLeavingApplicationFull;
//        AdRequest request = new AdRequest.Builder().Build();
//        this.interstitial.LoadAd(request);
//    }

//    private void HandleOnAdLoadedFull(object sender, EventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleAdLoaded event received");
//    }

//    private void HandleOnAdFailedToLoadFull(object sender, AdFailedToLoadEventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
//    }

//    private void HandleOnAdOpenedFull(object sender, EventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleAdOpened event received");
//    }

//    private void HandleOnAdClosedFull(object sender, EventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleAdClosed event received");
//    }

//    private void HandleOnAdLeavingApplicationFull(object sender, EventArgs args)
//    {
//        UnityEngine.Debug.Log("HandleAdLeavingApplication event received");
//    }

//    private void RequestRewardAdmob()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-6442067588290070/3912606618";

//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-7682150448917230/1795270898";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        this.rewardBasedVideo.LoadAd(new AdRequest.Builder().Build(), adUnitId);
//    }

//    private void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
//    }

//    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
//    }

//    private void HandleRewardBasedVideoOpened(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
//    }

//    private void HandleRewardBasedVideoStarted(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
//    }

//    private void HandleRewardBasedVideoClosed(object sender, EventArgs args)
//    {
//        this.RequestRewardAdmob();
//    }

//    private void HandleRewardBasedVideoRewarded(object sender, Reward args)
//    {
//        string type = args.Type;
//        double amount = args.Amount;
//        base.StartCoroutine(this.StartReward());
//    }

//    private IEnumerator StartReward()
//    {
//        yield return null;
//        if (this.adCallBack != null)
//        {
//            this.adCallBack();
//        }
//        yield break;
//    }

//    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
//    }

//    private bool ShowFullAdmob()
//    {
//        if (this.interstitial.IsLoaded())
//        {
//            this.IsShowInter = true;
//            this.interstitial.Show();
//            this.RequestInterstitial();
//            return true;
//        }
//        return false;
//    }

//    //private bool ShowFullUnity()
//    //{
//    //    if (Advertisement.IsReady())
//    //    {
//    //        this.IsShowInter = true;
//    //        Advertisement.Show(this.placementIdVideoNormal);
//    //        return true;
//    //    }
//    //    return false;
//    //}

//    public void ShowInterstitial()
//    {
//        if (!Preference.Instance.DataGame.NoAds)
//        {
//            if (this.ShowFullAdmob())
//            {
//                Debug.Log("ADS_ShowFullAdmob");
//                GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Interstitial_Admob");
//            }
//            else {
//                Debug.Log("ADS_ShowFullUnity");
//                //this.ShowFullUnity();
//                GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "Interstitial_Unity");
//            }
//        }
//    }

//    private BannerView bannerView;

//    private BannerView bannerView2;

//    public string placementId = "rewardedVideo";

//    public string placementIdVideoNormal = "video";

//    public bool testNoAdAvailable;

//    private AdsController.AdCallBack adCallBack;


//    public bool IsShowBanner;

//    public bool IsLoadBanner;

//    private InterstitialAd interstitial;

//    private RewardBasedVideoAd rewardBasedVideo;

//    public bool IsShowInter;

//    public delegate void AdCallBack();
//}
