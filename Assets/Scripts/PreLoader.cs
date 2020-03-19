using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLoader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimumLogoTime = 0.4f; // miniumum time of that scene

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1;
        // Pre load the game
        GameManager.Instance.LoadSave();
        // loadtime
        if (Time.time < minimumLogoTime)
            loadTime = minimumLogoTime;
        else
            loadTime = Time.time;
    }

    private void Update()
    {
        // Fade in
        if (Time.time < minimumLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }
        // fade out
        if (Time.time > minimumLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minimumLogoTime;
            if (fadeGroup.alpha >= 1)
            {
                GameManager.Instance.LoadScene("Menu");
            }
        }

    }
}
