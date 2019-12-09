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
            Debug.Log("Staty: " + state.ToString());
        }
        //      Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //     Instance = this;
    }
    

    void Start()
    {
        currentLevel = 0;
        scenesInBuild = new List<string>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
        }
    }
    
    void Update()
    {
        
    }
}
