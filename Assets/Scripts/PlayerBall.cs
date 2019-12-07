using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBall : MonoBehaviour
{
    public Rigidbody2D hook;
    public Text readyText;
    
    public float releaseTime = .15f;
    public float maxDragDistance = 2f;
    public float activeColierRange = 0.2f;

    public bool isPressed = false;
    private Vector2 pos;

    private Vector3 velocity;
    public static bool isMove { get; set; } = false;
    private float speedToGrab = 0.15f;
    private float timeToGrab = 0.75f;
    private float timer;
    private float gravityScale = 1.0f;

    
    private CircleCollider2D circleCol2D;
    private SpringJoint2D springJoint2D;
    private Rigidbody2D rb;
    private TrailRenderer tr;

    private void Start()
    {
        circleCol2D = GetComponent<CircleCollider2D>();
        springJoint2D = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        StopBall();

    }

    private void Update()
    {
        if (isMove)
        {
            circleCol2D.enabled = true;
            EnableGrab();
        }
        else if (isPressed)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(pos, hook.position) > maxDragDistance)
            {
                rb.position = hook.position + (pos - hook.position).normalized * maxDragDistance;
            }
            else
            {
                rb.position = pos;
            }
        }
        
    }

    private void OnMouseDown()
    {
        if (!isMove)
        {
        //    hookObject.SetActive(true);
            isPressed = true;
            rb.isKinematic = true;
        }
    }

    private void OnMouseUp()
    {
        if (!isMove)
        {
            rb.gravityScale = gravityScale;
            isPressed = false;
            rb.isKinematic = false;
            tr.enabled = true;

            StartCoroutine(Release());
        }
    }

    IEnumerator Release()
    {
        circleCol2D.enabled = false;
        yield return new WaitForSeconds(releaseTime);

        springJoint2D.enabled = false;
        // this.enabled = false;
        isMove = true;
        timer = timeToGrab;
        readyText.color = Color.black;
        
    }

    private void EnableGrab()
    {
        velocity = rb.velocity;
        if (velocity.magnitude <= speedToGrab)
        {
            timer -= Time.deltaTime;
      //      Debug.Log(" Speed to stop: " + velocity.magnitude + " Timer: " + timer);
            if (timer <= 0)
            {
                StopBall();
            }
        }
        else
        {
            timer = timeToGrab;
       //     Debug.Log("Vel: " + velocity + " Speed: " + velocity.magnitude);
        }
    }

    public void Freez()
    {
        StopBall();
        rb.gravityScale = 0.0f;
    }

    public void Wind()
    {
        rb.gravityScale = 0.05f;
    }

    public void StopBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        hook.transform.position = transform.position;
        springJoint2D.enabled = true;
        readyText.color = Color.green;
   //     tr.enabled = false;
        isMove = false;
    }

}
