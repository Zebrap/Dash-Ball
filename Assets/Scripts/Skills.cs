using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField]
    public Sprite[] skillSprites;

    public Sprite blankSkillSprite;
    
    [SerializeField]
    public Image[] showSkills;

    public GameObject winPanel; 

    public PlayerBall player;
    private Queue<SkillsNames> skillsQueue = new Queue<SkillsNames>();
    

    void Start()
    {

    }
    
    void Update()
    {
        
    }
    public void addSkills()
    {
        addSkill(SkillsNames.Freeze);
    }

    public void addSkill(SkillsNames skillname)
    {
        if(showSkills.Length > skillsQueue.Count)
        {
            skillsQueue.Enqueue(skillname);
            switch (skillname)
            {
                case SkillsNames.Freeze:
                    showSkills[skillsQueue.Count-1].GetComponent<Image>().sprite = skillSprites[0];
                    Debug.Log("add to stack: "+SkillsNames.Freeze);
                    break;
                case SkillsNames.Wind:
                    showSkills[skillsQueue.Count - 1].GetComponent<Image>().sprite = skillSprites[1];
                    Debug.Log("add to stack: " + SkillsNames.Wind);
                    break;
                default:
                    Debug.Log("No skill");
                    break;
            }
        }
        else
        {
            Debug.Log("Full stack");
        }
    }
    
    public void useSkill()
    {
        if (skillsQueue.Count > 0)
        {
            switch (skillsQueue.Dequeue())
            {
                case SkillsNames.Freeze:
                    player.Freez();
                    Debug.Log(SkillsNames.Freeze);
                    changeSpriteAfterUse();
                    break;
                case SkillsNames.Wind:
                    player.Wind();
                    Debug.Log(SkillsNames.Wind);
                    changeSpriteAfterUse();
                    break;
                default:
                    Debug.Log("No skill");
                    break;
            }
        }
        else
        {
            Debug.Log("No skill stack");
        }
        
    }

    void changeSpriteAfterUse()
    {
        for(int i=0; i < showSkills.Length-1; i++)
        {
            showSkills[i].GetComponent<Image>().sprite = showSkills[i + 1].GetComponent<Image>().sprite;
        }
        showSkills[showSkills.Length-1].GetComponent<Image>().sprite = blankSkillSprite;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player.isPressed)
        {
            if(collision.tag.ToString() == "Win")
            {
                winPanel.SetActive(true);
            }
            else
            {
                SkillsNames tag = (SkillsNames)Enum.Parse(typeof(SkillsNames), collision.tag);
                addSkill(tag);
            }
            
        }
      
    }
}
