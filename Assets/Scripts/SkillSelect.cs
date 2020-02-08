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
    private SkillsNames[] skillList = new SkillsNames[3] { SkillsNames.Blank, SkillsNames.Blank, SkillsNames.Blank };

    void Start()
    {
        // TODO load from profil skill list
        // TODO requirements

        foreach (Sprite sprite in spriteSkill)
        {
            Button button = Instantiate(buttonSkill) as Button;
            button.image.sprite = sprite;
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
    
    void Update()
    {
        
    }

    public void SetSkillPostion(int vlaue)
    {
        queueValue = vlaue;
    }

    public void SetSkillToQueue(SkillsNames skillname, Sprite sprite)
    {
       skillList[queueValue] = skillname;
        skillListPanel.transform.GetChild(queueValue).GetComponent<Image>().sprite = sprite;
    }
}
