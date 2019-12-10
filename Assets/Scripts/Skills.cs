﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class Skills : MonoBehaviour
{
    [SerializeField]
    public Sprite[] skillSprites;

    public Sprite blankSkillSprite;
    
    [SerializeField]
    public Image[] showSkills;

    public GameObject winPanel;
    public GameObject losePanel;

    public PlayerBall player;
    private Queue<SkillsNames> skillsQueue = new Queue<SkillsNames>();

    
    void Start()
    {

    }
    
    void Update()
    {

        if (player.transform.position.y < -10)
        {
            player.gameObject.SetActive(false);
            losePanel.SetActive(true);
        }
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
                    AddSpriteSkill(0);
                //    Debug.Log("add to stack: "+SkillsNames.Freeze);
                    break;
                case SkillsNames.Wind:
                    AddSpriteSkill(1);
                //    Debug.Log("add to stack: " + SkillsNames.Wind);
                    break;
                case SkillsNames.Stone:
                    AddSpriteSkill(2);
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
                //    Debug.Log(SkillsNames.Freeze);
                    changeSpriteAfterUse();
                    break;
                case SkillsNames.Wind:
                    player.Wind();
               //     Debug.Log(SkillsNames.Wind);
                    changeSpriteAfterUse();
                    break;
                case SkillsNames.Stone:
                    player.Stone();
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
                 if(GameManager.Instance.currentLevel == GameManager.Instance.state.levelReached)
                    {
                        GameManager.Instance.state.levelReached++;
                        SaveGame.Save<PlayerData>("PlayerData", GameManager.Instance.state);
                    }
                winPanel.SetActive(true);
                player.StopBall();
            }
            else
            {
                if(Enum.TryParse(collision.tag, out SkillsNames result))
                {
                  //  SkillsNames tag = (SkillsNames)Enum.Parse(typeof(SkillsNames), collision.tag);
                    addSkill(result);
                }
                else
                {
                    Debug.Log("Not enum tag: " + collision.tag);
                }
            }
            
        }
      
    }

    private void AddSpriteSkill(int skillSpriteNumber)
    {
        showSkills[skillsQueue.Count - 1].GetComponent<Image>().sprite = skillSprites[skillSpriteNumber];
    }
}
