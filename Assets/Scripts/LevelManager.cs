using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private void Start()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        string levelName = GameManager.Instance.currentLevel.ToString();
        if (GameManager.Instance.scenesInBuild.Contains(levelName)){
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogWarning("Load Scene Failed. " + levelName + " not found.");
            BackToMenu();
        }
    }
    
    public void NextLevel()
    {
        GameManager.Instance.currentLevel++;
        ResetLeavel();
    }

    public void ResetLeavel()
    {
        SceneManager.LoadScene("Level");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
