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
    //    LoadSave();
    }

    void Start()
    {
        // TODO
    }

    public void LoadSave() {
        //       SaveGame.Clear();
        if (SaveGame.Exists("PlayerData"))
        {
            state = SaveGame.Load<PlayerData>("PlayerData");
            //          Debug.Log("Staty: " + state.ToString());
        }
        else
        {
            state = new PlayerData();
        }
        //      Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Init score list
        while (state.score.Count < state.levelReached)
        {
            state.score.Add(0);
        }
        currentLevel = 0;
        scenesInBuild = new List<string>();
        for (int i = 3; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
        }
    }

    public void ResetSave()
    {
        SaveGame.Clear();
        LoadSave();
        SceneManager.LoadScene("Menu");
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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /*
    public bool BuySkin(int index, int cost)
    {
        if (state.cash >= cost)
        {
            // buy
            state.cash -= cost;
            // Save
            return true;
        }
        else
        {
            return false;
        }
    }
    */
    public bool Buy(int cost)
    {
        if (state.cash >= cost)
        {
            state.cash -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UnlockSkin(int index)
    {
        // Toggle on the bit at index
        state.skinOwned |= 1 << index;
    }

    public void ChangeActiveSkin(int index)
    {
        state.activeSkin = index;
        Save();
    }

    public void Save()
    {
        SaveGame.Save<PlayerData>("PlayerData", state);
    }
}
