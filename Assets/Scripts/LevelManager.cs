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
        try
        {
            targetPos = GameObject.Find("Win").transform.position;
     //       Debug.Log(targetPos.ToString());
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning("'Win' obecjt does not exist: "+e);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }

    }

  /*  private void Pointer()
    {
        // 1
        Vector3 dir = Camera.main.transform.InverseTransformPoint(targetPos);
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        angle += 180;
        pointerArrow.transform.localEulerAngles = new Vector3(0, 0, angle);

        // 2
        Vector3 dir = (targetPos - Camera.main.transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        angle += 180;
        pointerArrow.transform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 20f;
        Vector3 targetPostionScreenPoint = Camera.main.WorldToScreenPoint(targetPos);
        bool isOffScreen = targetPostionScreenPoint.x <= borderSize || targetPostionScreenPoint.x >= Screen.width - borderSize || targetPostionScreenPoint.y < borderSize || targetPostionScreenPoint.y >= Screen.height - borderSize;
        if (isOffScreen)
        {
            Vector3 capTagetScreenPos = targetPostionScreenPoint;
            if (capTagetScreenPos.x <= 0) capTagetScreenPos.x = 0f;
            if (capTagetScreenPos.x >= Screen.width) capTagetScreenPos.x = Screen.width;
            if (capTagetScreenPos.y <= 0) capTagetScreenPos.y = 0f;
            if (capTagetScreenPos.y >= Screen.height) capTagetScreenPos.y = Screen.height;

            Vector3 pointerWorldPos = Camera.main.ScreenToWorldPoint(capTagetScreenPos);
            pointerArrow.position = pointerWorldPos;
            pointerArrow.localPosition = new Vector3(pointerArrow.localPosition.x, pointerArrow.localPosition.y, 0f);
        }
    }
*/
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
