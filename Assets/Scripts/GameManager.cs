using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int currentLevel;
    public List<string> scenesInBuild { get; set; }
    public PlayerData state = new PlayerData();
    public Animator aniamtor;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
      //  SaveGame.Clear();
        if (SaveGame.Exists("PlayerData"))
        {
            state = SaveGame.Load<PlayerData>("PlayerData");
  //          Debug.Log("Staty: " + state.ToString());
        }
        //      Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //     Instance = this;

        // Init score list
        while(state.score.Count < state.levelReached)
        {
            state.score.Add(0);
        }

        currentLevel = 0;
        scenesInBuild = new List<string>();
        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
        }
    }
    

    void Start()
    {

    }

    void Update()
    {
        
    }


    public void FadeToNextScane()
    {

    }
    public void FadeInScane()
    {
        aniamtor.SetTrigger("FadeIn");
    }

    public void FadeToScane()
    {
        aniamtor.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("Level");

    }
}
