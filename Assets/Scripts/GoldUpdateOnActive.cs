using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUpdateOnActive : MonoBehaviour
{
    public Text textGold;

    private void OnEnable()
    {
        if(GameManager.Instance != null)
        {
            textGold.text = "Gold: " + GameManager.Instance.state.cash;
        }
    }

    private void Start()
    {
        textGold.text = "Gold: " + GameManager.Instance.state.cash;
    }
}
