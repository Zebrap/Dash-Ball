using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    private Transform target;
    public float HideDistance;

    private void Start()
    {
        target = GameObject.Find("Win").transform;
    }

    private void Update()
    {
        var dir = target.position - transform.position;
        if(dir.magnitude > HideDistance)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle += 180;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}
