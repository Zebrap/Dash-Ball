using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {

        // load up the level
        SceneManager.LoadScene(GameManager.Instance.currentLevel.ToString(), LoadSceneMode.Additive);
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
