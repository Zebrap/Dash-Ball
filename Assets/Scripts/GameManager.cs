using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int currentLevel = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
  //      Screen.sleepTimeout = SleepTimeout.NeverSleep;
   //     Instance = this;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
