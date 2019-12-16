using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public int levelReached { get; set; }
    public List<int> score;
    public int cash;

    public PlayerData()
    {
        levelReached = 0;
        score = new List<int>();
        cash = 0;
    }

}
