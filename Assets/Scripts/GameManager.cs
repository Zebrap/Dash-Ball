using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    public int currentLevel = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
  //      Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Instance = this;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
