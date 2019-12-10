using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button selectLevelButton;
    public GameObject levelContainer;

    public GameObject mainMenu;
    public GameObject levelMenu;

    void Start()
    {
        InitLevels();
    }
    
    void Update()
    {
        
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
            button.onClick.AddListener(() => StartLevel(index));
            button.transform.SetParent(levelContainer.transform);
            if(GameManager.Instance.state.levelReached < index)
            {
                button.interactable = false;
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

    public void ShowMenuLevel()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
   
    public void ShowMainMenu()
    {
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
