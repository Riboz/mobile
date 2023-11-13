using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.SocialPlatforms.Impl;
public class PrizedAdMob : MonoBehaviour
{
    

#if UNITY_ANDROID
  private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string _adUnitId = "unused";
#endif

  private RewardedAd _rewardedAd;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        LoadRewardedAd();
    
    }
    public void LoadRewardedAd()
  {
      // Clean up the old ad before loading a new one.
      if (_rewardedAd != null)
      {
            _rewardedAd.Destroy();
            _rewardedAd = null;
      }

      Debug.Log("Loading the rewarded ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest();

      // send the request to load the ad.
      RewardedAd.Load(_adUnitId, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("Rewarded ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Rewarded ad loaded with response : "
                        + ad.GetResponseInfo());

              _rewardedAd = ad;
          });
    
    }
    public void DoNotShow()
    {
         GameManager.Mistake = 0;
         GameManager.Score = 0;
         for(int i=0;i<GameManager.manager.health.Length;i++){GameManager.manager.health[i].sprite = GameManager.manager.healthSprite[0];} 
         PlayerPrefs.SetInt("scorePoint",GameManager.Score);  
         this.gameObject.SetActive(false);
        GameManager.manager.NewLevelCreation();
          GameManager.manager.scoreText.text = "0";
    }
    public void ShowRewardedAd()
{
    const string rewardMsg =
        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

    if (_rewardedAd != null && _rewardedAd.CanShowAd())
    {
        _rewardedAd.Show((Reward reward) =>
        {
            // TODO: Reward the user.
            GameManager.Mistake = 0;
            this.gameObject.SetActive(false);
            for(int i=0;i<GameManager.manager.health.Length;i++){GameManager.manager.health[i].sprite = GameManager.manager.healthSprite[0];} 
            GameManager.manager.NewLevelCreation();
            Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
        });
    }
}
}
