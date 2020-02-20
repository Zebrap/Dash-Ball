using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    void Start()
    {
        
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Player Gains Gold");
                break;
            case ShowResult.Skipped:
                Debug.Log("Player Skip ad");
                break;
            case ShowResult.Failed:
                Debug.Log("Player failed to lunch the ad");
                break;
        }
    }
}
