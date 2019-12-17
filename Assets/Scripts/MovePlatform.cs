using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector3 orginalPosition;
  //  [SerializeField]
    public Vector3 Velocity;
    private float endPosR;
    private float endPosL;
    private bool right = true;

    void Start()
    {
        orginalPosition = transform.position;
        endPosR = orginalPosition.x + 1;
        endPosL = orginalPosition.x - 1;
    }
    

    private void FixedUpdate()
    {
        if (right)
        {
            transform.position += (Velocity * Time.deltaTime);
            if (transform.position.x >= endPosR)
                right = !right;
        }
        else
        {
            transform.position -= (Velocity * Time.deltaTime);
            if (transform.position.x <= endPosL)
                right = !right;
        }
    }
}
