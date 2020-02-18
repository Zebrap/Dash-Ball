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
    public int[] requrementsLevelForPlace = { 5, 10, 15 };
    public int[] requrementsGoldForPlace = { 6, 10, 18 };
    public Text textGoldBuy;
    public Text textLevelBuy;

    private int queueValue = 0;
    [SerializeField]
    //private SkillsNames[] skillList = new SkillsNames[3] { SkillsNames.Blank, SkillsNames.Blank, SkillsNames.Blank };
    private Vector3 scaleSprite = new Vector3(0.9f, 0.9f);
    public Color32 baseColor = new Color32(255, 255, 255, 50);
    public Color32 activeColor = new Color32(20, 250, 0, 100);
    public Color32 transparentUnableSkill = new Color32(255, 255, 255, 40);
    private Color32 unableSkill = new Color32(255, 255, 255, 255);

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
        for (int i = 0; i < 3; i++)
        {
            if (checkUnenablePosSkill(i))
            {
                skillListPanel.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                skillListPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = transparentUnableSkill;
            }
            else
            {
                skillListPanel.transform.GetChild(0).GetComponent<Image>().color = activeColor;
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
    }

    public void SelectSkillIPosition(int value)
    {
        if (checkUnenablePosSkill(value))
        {
            OpenBuyWindow(GameManager.Instance.state.skillsPlace);
        }
        else
        {
            ChangeSkillPos(value);
        }
    }

    public void SetSkillToQueue(SkillsNames skillname, Sprite sprite)
    {
        if (!checkUnenablePosSkill(queueValue))
        {
            GameManager.Instance.state.skillsOnStart[queueValue] = skillname;
            skillListPanel.transform.GetChild(queueValue).GetChild(0).GetComponent<Image>().sprite = sprite;
            skillListPanel.transform.GetChild(queueValue).GetChild(0).GetComponent<RectTransform>().localScale = scaleSprite;
        }
    }

    public void OpenBuyWindow(int pos)
    {
        buyWindow.SetActive(true);
        textGoldBuy.text = "Gold : " + requrementsGoldForPlace[pos];
        textLevelBuy.text = "Level : " + requrementsLevelForPlace[pos];
    }

    public void CloseBuyWindow()
    {
        buyWindow.SetActive(false);
    }

    public void BuyPlaceForSkills()
    {
        int pos = GameManager.Instance.state.skillsPlace;
        // check requiremts
        if(requrementsGoldForPlace[pos] <= GameManager.Instance.state.cash && requrementsLevelForPlace[pos] <= GameManager.Instance.state.levelReached )
        {
            // update state -gold +add place
            if (GameManager.Instance.Buy(requrementsGoldForPlace[pos]))
            {
                GameManager.Instance.state.skillsPlace++;
                // save profile
                GameManager.Instance.Save();
                skillListPanel.transform.GetChild(pos).GetChild(1).gameObject.SetActive(false);
                skillListPanel.transform.GetChild(pos).GetChild(0).GetComponent<Image>().color = unableSkill;
                ChangeSkillPos(pos);
                // Update gold
            }
        }
        CloseBuyWindow();
    }

    private bool checkUnenablePosSkill(int value)
    {
        if (value >= GameManager.Instance.state.skillsPlace)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ChangeSkillPos(int value)
    {
        skillListPanel.transform.GetChild(queueValue).GetComponent<Image>().color = baseColor;
        queueValue = value;
        skillListPanel.transform.GetChild(value).GetComponent<Image>().color = activeColor;
    }
}
