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
    private float timeToGrab = 0.35f;
    private float timer;
    private float gravityScale = 1.0f;
    private float ballMass;
    public bool drag = false;
    
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

    private float flyGrav = -0.1f;
    private float flyTimer = 2.0f;
    

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
                    line.enabled = false;
                    tr.enabled = true;

                    throwCounter++;
                    rb.gravityScale = gravityScale;
                    isPressed = false;
                    rb.isKinematic = false;
                    changeThrowText();

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
        rb.gravityScale = freezGravScale;
    }

    public void Wind()
    {
        rb.gravityScale = windGravScale;
    }

    public void Stone()
    {
        rb.mass = rb.mass * stoneMass;
    }

    public void Fly()
    {
        rb.gravityScale = flyGrav;
        StartCoroutine(FlyTimer());
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
    }

    public void Die()
    {
        isMove = false;
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
