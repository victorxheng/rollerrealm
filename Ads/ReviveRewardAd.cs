using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using ChartboostSDK;

public class ReviveRewardAd : MonoBehaviour {

    public ControllerScriptLvl6 ControllerScriptLvl6;
    public TextMeshProUGUI UseReviveText;
    
    // Use this for initialization
    void Start()
    {
        Chartboost.cacheRewardedVideo(CBLocation.MainMenu);
    }

    public void showRewardAd()
    {
        if (Chartboost.hasRewardedVideo(CBLocation.Default)) {
            Chartboost.showRewardedVideo(CBLocation.locationFromName("Default"));
        }
        else
        {
            Chartboost.cacheRewardedVideo(CBLocation.MainMenu);
            UseReviveText.text = "Failed, Try Again";
        }
    }
    
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        UseReviveText.text = "Ad Successful";
        ControllerScriptLvl6.Revive();
    }

    void didCompleteRewardedVideo(CBLocation location, int reward)
    {
        UseReviveText.text = "Ad Successful";
        ControllerScriptLvl6.Revive();
    }

}

