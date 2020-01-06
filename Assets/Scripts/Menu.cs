﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button selectLevelButton;
    public GameObject levelContainer;

    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject instructionMenu;

    public Text CashText;
    public Sprite fullStar;

    void Start()
    {
        InitLevels();
        InitCash();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void StartLevel(int scane)
    {
        GameManager.Instance.currentLevel = scane;
        GameManager.Instance.FadeToScane();
    }

    private void InitLevels()
    {
        int levelIndex = 0;
        foreach (string name in GameManager.Instance.scenesInBuild)
        {
            
            int index = levelIndex;
            Button button = Instantiate(selectLevelButton) as Button;
            button.GetComponentInChildren<Text>().GetComponentInChildren<Text>().text = name;
       //     GameManager.Instance.state.score[index]
            button.onClick.AddListener(() => StartLevel(index));
            button.transform.SetParent(levelContainer.transform);
            // not enable
            if (GameManager.Instance.state.levelReached < index)
            {
                button.interactable = false;
            }
            else if(GameManager.Instance.state.score.Count > index)
            {
                // complate level, add stars
                for (int i = 0; i < GameManager.Instance.state.score[index]; i++)
                {
                    button.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = fullStar;
                }
            }
            levelIndex++;
        }
        // ToDo ?
            /*  private int ConvertStringToInt(string intString)
        {
            int i = 0;
            if (!Int32.TryParse(intString, out i))
            {
                i = 0;
            }
            return i;
        }
        */
    }

    private void InitCash()
    {
        CashText.text = "Gold: " + GameManager.Instance.state.cash;
    }

    public void ShowMenuLevel()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void ShowInstructionMenu()
    {
        mainMenu.SetActive(false);
        instructionMenu.SetActive(true);
    }
    
    public void Back(GameObject menu)
    {
        menu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ResetSave()
    {
        GameManager.Instance.ResetSave();
    }
}
