using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterAtItem : MonoBehaviour
{
    public RectTransform sr;
    private void OnEnable()
    {
        SetView();
    }

    private void SetView()
    {
        if (GameManager.Instance.state.levelReached < 6)
        {
            sr.offsetMax = new Vector2(sr.offsetMax.x, 0);
        }
        else
        {
            // TODO ?
            sr.offsetMax = new Vector2(sr.offsetMax.x, (((GameManager.Instance.state.levelReached)-4)/2) * 280);
        }
           //      sr.position = new Vector2(sr.offsetMax.x, -transform.GetChild(GameManager.Instance.state.levelReached).GetComponent<RectTransform>().anchoredPosition.y);
           //    sr.offsetMax = new Vector2(sr.offsetMax.x, -transform.GetChild(GameManager.Instance.state.levelReached).GetComponent<RectTransform>().anchoredPosition.y);
    }
}
