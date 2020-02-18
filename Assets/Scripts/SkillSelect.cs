using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    public Button buttonSkill;
    [SerializeField]
    public Sprite[] spriteSkill;
    public GameObject skillsContainer;
    public GameObject skillListPanel;
    public GameObject buyWindow;
    public int[] requrementsLevelForSkills = { 0, 5, 10, 15, 40, 45, 50 };

    private int queueValue = 0;
    [SerializeField]
    //private SkillsNames[] skillList = new SkillsNames[3] { SkillsNames.Blank, SkillsNames.Blank, SkillsNames.Blank };
    private Vector3 scaleSprite = new Vector3(0.9f, 0.9f);
    public Color32 baseColor = new Color32(255, 255, 255, 50);
    public Color32 activeColor = new Color32(20, 250, 0, 100);
    public Color32 transparentUnableSkill = new Color32(255, 255, 255, 40);

    void Start()
    {

        // TODO load from profil skill list
        // TODO requirements

        InitSkillsButtons();
        InitLoadSkills();
    }

    private void InitSkillsButtons()
    {
        int requrementsI = 0;
        foreach (Sprite sprite in spriteSkill)
        {
            Button button = Instantiate(buttonSkill) as Button;
            button.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            if(requrementsLevelForSkills[requrementsI] > GameManager.Instance.state.levelReached)
            {
                button.transform.GetChild(1).GetComponent<Text>().text = "Requires level " + requrementsLevelForSkills[requrementsI];
                button.transform.GetChild(0).GetComponent<Image>().color = transparentUnableSkill; 
            }
            else
            {
                if (Enum.TryParse(sprite.name, out SkillsNames result))
                {
                    button.onClick.AddListener(() => SetSkillToQueue(result, sprite));
                }
                else
                {
                    button.onClick.AddListener(() => SetSkillToQueue(SkillsNames.Blank, sprite));
                }
            }
            button.transform.SetParent(skillsContainer.transform);
            requrementsI++;
        }
    }

    private void InitLoadSkills()
    {
        skillListPanel.transform.GetChild(0).GetComponent<Image>().color = activeColor;
        for (int i = 0; i < 3; i++)
        {
            foreach (Sprite sprite in spriteSkill)
            {
                if (sprite.name == GameManager.Instance.state.skillsOnStart[i].ToString())
                {
                    skillListPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = sprite;
                    break;
                }
            }
        }
    }

    public void SelectSkillIPosition(int vlaue)
    {
        if (vlaue>0)
        {
            OpenBuyWindow();
        }
        else
        {
            skillListPanel.transform.GetChild(queueValue).GetComponent<Image>().color = baseColor;
            queueValue = vlaue;
            skillListPanel.transform.GetChild(vlaue).GetComponent<Image>().color = activeColor;
        }
    }

    public void SetSkillToQueue(SkillsNames skillname, Sprite sprite)
    {
       GameManager.Instance.state.skillsOnStart[queueValue] = skillname;
       skillListPanel.transform.GetChild(queueValue).GetChild(0).GetComponent<Image>().sprite = sprite;
       skillListPanel.transform.GetChild(queueValue).GetChild(0).GetComponent<RectTransform>().localScale = scaleSprite;
    }

    public void OpenBuyWindow()
    {
        buyWindow.SetActive(true);
    }

    public void CloseBuyWindow()
    {
        buyWindow.SetActive(false);
    }

    public void BuyPlaceForSkills()
    {
        // check requiremts

        // update state -gold +add place

        // save profile
    }
}
