using System;
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

    private Reward rewardsLevel;
    private bool stateChange;

    public GameObject StarPanel;
    private float releaseTime = 0.6f;

    void Start()
    {
        stateChange = false;
    }
    
    void Update()
    {

        if (player.transform.position.y < -7 && !player.drag)
        {
            lose();
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
                case SkillsNames.Fly:
                    AddSpriteSkill(3);
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
        if (!player.isPressed && player.circleCol2D.enabled && PlayerBall.isMove)
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
                    case SkillsNames.Fly:
                        player.Fly();
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
        else
        {
            Debug.Log("You can't use the skill while dragging");
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
                win();
            }
            else if(collision.tag.ToString() == "Trap"){
                // if trap immunity
                lose();
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

    private void win()
    {
        int currentLevel = GameManager.Instance.currentLevel;

        // Score calc
        rewardsLevel = GameObject.Find("RewardManager").GetComponent<Reward>();
        int stars = 0;
        if(player.throwCounter <= rewardsLevel.topTime)
        {
            stars = 3;
        }
        else if(player.throwCounter <= rewardsLevel.midTime)
        {
            stars = 2;
        }
        else if(player.throwCounter <= rewardsLevel.lowTime)
        {
            stars = 1;
        }
        AddScore(currentLevel, stars);
        // ToDo drop levelReached in future - change to state.score.Count
        if (currentLevel == GameManager.Instance.state.levelReached)
        {
            GameManager.Instance.state.levelReached++;
            stateChange = true;
        }
        // save data
        if (stateChange)
        {
            SaveGame.Save<PlayerData>("PlayerData", GameManager.Instance.state);
        }
        player.StopBall();
        winPanel.SetActive(true);
        AnimStar(0, stars);
    }

    private void lose()
    {
        // player.gameObject.SetActive(false);
        player.Die();
        losePanel.SetActive(true);
    }

    private void AddScore(int currentLevel, int stars)
    {
        int currentStars = 0;
        if (currentLevel == GameManager.Instance.state.levelReached)
        {
            GameManager.Instance.state.score.Add(stars);
        }
        else
        {
            currentStars = GameManager.Instance.state.score[currentLevel];
            if(currentStars < stars)
            {
                GameManager.Instance.state.score[currentLevel] = stars;
            }
        }
        int differentScore = GameManager.Instance.state.score[currentLevel] - currentStars;
        if (differentScore > 0)
        {
            GameManager.Instance.state.cash += differentScore;
            stateChange = true;
        }
        // Add cash
    }
    
    private void AnimStar(int index, int stars)
    {
        if (stars > (index))
        {
            StarPanel.transform.GetChild(index).GetComponent<Animator>().enabled = true;
            StarPanel.transform.GetChild(index).GetComponent<Animator>().SetBool("StartHide", true);
            StartCoroutine(ReleaseAnim(index + 1, stars));
        }
    }

    IEnumerator ReleaseAnim(int i, int stars)
    {
        yield return new WaitForSeconds(releaseTime);
        AnimStar(i, stars);
    }
}
