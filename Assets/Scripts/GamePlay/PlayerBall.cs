﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBall : MonoBehaviour
{
    public Rigidbody2D hook;
    public Text readyText;
    
    public float releaseDragTime = .18f;
    public float maxDragDistance = 2f;
    public float activeColierRange = 0.2f;

    public bool isPressed = false;
    private Vector2 pos;

    private Vector3 velocity;
    public static bool isMove { get; set; } = false;
    private float speedToGrab = 0.15f;
    private float timeToGrab = 0.35f;
    private float timer;
    private float gravityScale = 1.0f;
    private float ballMass;
    public bool drag = false;
    public bool canUseSkinn { get; set; } = false;

    public CircleCollider2D circleCol2D;
    private SpringJoint2D springJoint2D;
    private Rigidbody2D rb;
    private TrailRenderer tr;

    public int throwCounter;
    public Text throwText;

    private LineRenderer line;

    // skills parameters
    private float freezGravScale = 0.0f;
    private float windGravScale = 0.3f;
    private float stoneMass = 20.0f;

    private float flyGrav = -0.3f;
    private float flyTimer = 2.0f;

    private Vector3 baseScale;
    private bool scalling = false;
    private float scaleValue = 0.3f;

    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip skillsound1;
    public AudioClip skillsound2;
    public AudioClip skillsound3;
    public AudioClip skillsound4;
    public AudioClip skillsound5;
    public AudioClip winSound;
    public AudioClip gameOverSound;

    private void Awake()
    {
        circleCol2D = GetComponent<CircleCollider2D>();
        springJoint2D = GetComponent<SpringJoint2D>();
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Skins/Ball"+GameManager.Instance.state.activeSkin.ToString());
        ballMass = rb.mass;
        throwCounter = 0;
        baseScale = transform.localScale;
    }

    private void Start()
    {
        changeThrowText();
        StopBall();
    }

    private void Update()
    {
        if (isMove)
        {
            circleCol2D.enabled = true;
            EnableGrab();

            if (scalling)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * scaleValue, Time.deltaTime * 3f);
                if (transform.localScale.x <= baseScale.x * scaleValue)
                {
                    scalling = false;
                }
            }
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
                        line.enabled = true;
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
                    canUseSkinn = true;
                    line.enabled = false;
                    tr.enabled = true;

                    throwCounter++;
                    rb.gravityScale = gravityScale;
                    isPressed = false;
                    rb.isKinematic = false;
                    changeThrowText();
                    SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);

                    StartCoroutine(Release());
                }
            }
        }
        
    }

    private void DrawHook()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.GetChild(0).position);
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
        DrawHook();
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
        yield return new WaitForSeconds(releaseDragTime);

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
        rb.gravityScale = freezGravScale;
        SoundManager.instance.PlaySingle(skillsound1);
    }

    public void Wind()
    {
        rb.gravityScale = windGravScale;
        SoundManager.instance.PlaySingle(skillsound2);
    }

    public void Stone()
    {
        rb.mass = rb.mass * stoneMass;
        SoundManager.instance.PlaySingle(skillsound3);
    }

    public void Fly()
    {
        rb.gravityScale = flyGrav;
        /*
        if(rb.velocity.y < 0)
        {
            rb.AddForce(new Vector2(0, 1)*(-rb.velocity.y)*10);
        }*/
        StartCoroutine(FlyTimer());
        SoundManager.instance.PlaySingle(skillsound4);
    }

    public void SmallSize()
    {
        rb.mass = rb.mass * 0.2f;
        scalling = true;
        SoundManager.instance.PlaySingle(skillsound5);
        //  transform.localScale = new Vector3(transform.localScale.x * 0.3f, transform.localScale.y * 0.3f, 1f);
    }

    IEnumerator FlyTimer()
    {
        yield return new WaitForSeconds(flyTimer);
        if (isMove && rb.gravityScale==flyGrav)
        {
            rb.gravityScale = gravityScale;
        }
    }
    /*
    public void Bounce()
    {
        rb.sharedMaterial.bounciness = 0.5f;
    }*/

    public void StopBall()
    {
        canUseSkinn = false;
        rb.isKinematic = true;
        rb.mass = ballMass;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        rb.gravityScale = 0.0f;
        hook.transform.position = transform.position;
        springJoint2D.enabled = true;
        readyText.color = new Color(0, 0.95f, 0.1f);
        tr.enabled = false;
        isMove = false;
        transform.localScale = baseScale;
    }

    public void Die()
    {
        isMove = false;
        drag = true;
        circleCol2D.enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        rb.gravityScale = 0.0f;
    }


    private void changeThrowText()
    {
        throwText.text = throwCounter.ToString();
    }

}
