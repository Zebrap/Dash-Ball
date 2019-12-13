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
    private float ballMass;
    public bool drag = false;

    
    public CircleCollider2D circleCol2D;
    private SpringJoint2D springJoint2D;
    private Rigidbody2D rb;
    private TrailRenderer tr;

    private void Awake()
    {

        circleCol2D = GetComponent<CircleCollider2D>();
        springJoint2D = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        ballMass = rb.mass;
    }

    private void Start()
    {

        StopBall();
    }

    private void Update()
    {
        if (isMove)
        {
            circleCol2D.enabled = true;
            EnableGrab();
        }
        else{
            PlayerInput();
        }

        /*else if (isPressed)
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
        }*/
        
    }

    private void PlayerInput()
    {
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && !isPressed && !drag)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.name == "Player")
                    {
                        rb.isKinematic = true;
                        isPressed = true;
                        drag = true;
                        dragBall();
                    }
                }
            }
            else if (isPressed)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    dragBall();
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    rb.gravityScale = gravityScale;
                    isPressed = false;
                    rb.isKinematic = false;
                    tr.enabled = true;

                    StartCoroutine(Release());
                }
            }
        }
        
    }

    private void dragBall()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        if (Vector3.Distance(pos, hook.position) > maxDragDistance)
        {
            rb.position = hook.position + (pos - hook.position).normalized * maxDragDistance;
        }
        else
        {
            rb.position = pos;
        }
    }
    /*
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
        if (!isMove && isPressed)
        {
            rb.gravityScale = gravityScale;
            isPressed = false;
            rb.isKinematic = false;
            tr.enabled = true;

            StartCoroutine(Release());
        }
    }*/

    IEnumerator Release()
    {
        circleCol2D.enabled = false;
        yield return new WaitForSeconds(releaseTime);

        springJoint2D.enabled = false;
        // this.enabled = false;
        isMove = true;
        drag = false;
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
        rb.gravityScale = 0.3f;
    }

    public void Stone()
    {
        rb.mass = rb.mass * 20;
    }

    public void StopBall()
    {
        rb.mass = ballMass;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        hook.transform.position = transform.position;
        springJoint2D.enabled = true;
        readyText.color = Color.green;
   //     tr.enabled = false;
        isMove = false;
    }

}
