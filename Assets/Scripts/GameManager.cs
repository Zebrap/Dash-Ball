﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
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
