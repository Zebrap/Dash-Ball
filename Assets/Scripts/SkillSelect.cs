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

    private int queueValue = 0;
    [SerializeField]
    //private SkillsNames[] skillList = new SkillsNames[3] { SkillsNames.Blank, SkillsNames.Blank, SkillsNames.Blank };
    private Vector3 scaleSprite = new Vector3(0.9f, 0.9f);
    public Color32 baseColor = new Color32(255, 255, 255, 50);
    public Color32 activeColor = new Color32(20, 250, 0, 100);

    void Start()
    {

        // TODO load from profil skill list
        // TODO requirements

        InitSkillsButtons();
        InitLoadSkills();
    }

    private void InitSkillsButtons()
    {
        foreach (Sprite sprite in spriteSkill)
        {
            Button button = Instantiate(buttonSkill) as Button;
            button.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            //   button.image.sprite = sprite;
            if (Enum.TryParse(sprite.name, out SkillsNames result))
            {
                button.onClick.AddListener(() => SetSkillToQueue(result, sprite));
            }
            else
            {
                button.onClick.AddListener(() => SetSkillToQueue(SkillsNames.Blank, sprite));
            }
            button.transform.SetParent(skillsContainer.transform);
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

    public void SetSkillPostion(int vlaue)
    {
        skillListPanel.transform.GetChild(queueValue).GetComponent<Image>().color = baseColor;
        queueValue = vlaue;
        skillListPanel.transform.GetChild(vlaue).GetComponent<Image>().color = activeColor;
    }

    public void SetSkillToQueue(SkillsNames skillname, Sprite sprite)
    {
       GameManager.Instance.state.skillsOnStart[queueValue] = skillname;
       skillListPanel.transform.GetChild(queueValue).GetChild(0).GetComponent<Image>().sprite = sprite;
       skillListPanel.transform.GetChild(queueValue).GetChild(0).GetComponent<RectTransform>().localScale = scaleSprite;
    }
}
