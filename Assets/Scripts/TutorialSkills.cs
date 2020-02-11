using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSkills : MonoBehaviour
{
    private GameObject skillPanel;
    private Sprite startSprite;
    private bool changeText = true;
    public GameObject tutorialPanel;
    public Sprite useButtonSprite;

    void Start()
    {
        skillPanel = GameObject.Find("SkillsPanel");
        startSprite = skillPanel.transform.GetChild(0).GetComponent<Image>().sprite;

    }

    private void Update()
    {
        if(startSprite != skillPanel.transform.GetChild(0).GetComponent<Image>().sprite && changeText)
        {
            Debug.Log("Change");
            tutorialPanel.transform.GetChild(0).GetComponent<Text>().text = "You can use skills during the flight by click the button:";
            tutorialPanel.transform.GetChild(1).GetComponent<Image>().sprite = useButtonSprite;
            changeText = false;
        }
    }

}
