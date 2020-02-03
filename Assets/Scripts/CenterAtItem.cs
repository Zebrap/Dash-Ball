using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterAtItem : MonoBehaviour
{
    public RectTransform sr;
    private void OnEnable()
    {
        sr.offsetMax = new Vector2(sr.offsetMax.x, -transform.GetChild(GameManager.Instance.state.levelReached).GetComponent<RectTransform>().anchoredPosition.y);
    }
}
