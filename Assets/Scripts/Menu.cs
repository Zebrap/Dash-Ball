using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {

        for(int i=0; i< levelButtons.Length; i++)
        {
            if(i <= GameManager.Instance.state.levelReached)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
    
    void Update()
    {
        
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
