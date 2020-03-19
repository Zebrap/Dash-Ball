using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSkills : MonoBehaviour
{
    private GameObject skillButton;
    private GameObject skillsPanel;

    void Start()
    {
        skillButton = GameObject.Find("SkillButton");
        skillsPanel = GameObject.Find("SkillsPanel");
        skillButton.SetActive(false);
        skillsPanel.SetActive(false);
    }
    
}
