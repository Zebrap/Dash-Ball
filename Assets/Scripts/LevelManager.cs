using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject optionPanel;

    private Vector3 targetPos; 

    private float baseTimeScale = 1.0f;

    //    public Transform pointerArrow;

    private void Awake()
    {
        LoadScene();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }

    }
    
    public void LoadScene()
    {
        string levelName = GameManager.Instance.currentLevel.ToString();
    //    Debug.Log("Load: " + levelName);
        if (GameManager.Instance.scenesInBuild.Contains(levelName)){
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        }
        else
        {
            GameManager.Instance.currentLevel = 0;
            Debug.LogWarning("Load Scene Failed. " + levelName + " not found.");
            BackToMenu();
        }
        GameManager.Instance.FadeInScane();
    }
    
    public void NextLevel()
    {
        GameManager.Instance.currentLevel++;
        ResetLeavel();
    }

    public void ResetLeavel()
    {
        ChangeTimeScale(baseTimeScale);
        GameManager.Instance.FadeToScane();
    //    SceneManager.LoadScene("Level");
    }

    public void BackToMenu()
    {
        ChangeTimeScale(baseTimeScale);
        SceneManager.LoadScene("Menu");
    }

    public void OpenOption()
    {
        optionPanel.SetActive(true);
        Pause();
    }

    void Pause()
    {
        ChangeTimeScale(0f);

    }

    public void Resume()
    {
        optionPanel.SetActive(false);
        ChangeTimeScale(baseTimeScale);

    }

    private void ChangeTimeScale(float ts)
    {
        Time.timeScale = ts;
    }
}
