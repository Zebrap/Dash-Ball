using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public int levelReached { get; set; }
    public List<int> score;
    public int cash;
    public int skinOwned = 1;
    public int activeSkin = 0;
    public SkillsNames[] skillsOnStart;

    public PlayerData()
    {
        levelReached = 0;
        score = new List<int>();
        cash = 0;
        skinOwned = 1;
        activeSkin = 0;
        skillsOnStart = new SkillsNames[3] { SkillsNames.Blank, SkillsNames.Blank, SkillsNames.Blank };
    }

}
