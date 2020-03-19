using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    private Vector3 destination;
    private Vector3 delta;
    private Vector3 point;

    private float yPos = 0.4f;
    // Update is called once per frame
    private void Start()
    {
        point = Camera.main.WorldToViewportPoint(target.position);
        delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, yPos, point.z));
        destination = transform.position + delta;
    }
    private void FixedUpdate()
    {
        if (target)
        {
            if (PlayerBall.isMove)
            {
                point = Camera.main.WorldToViewportPoint(target.position);
                delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, yPos, point.z)); //(new Vector3(0.5, 0.5, point.z));
                destination = transform.position + delta;
            }
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
