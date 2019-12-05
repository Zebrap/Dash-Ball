using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public TrailRenderer tr;
    public Rigidbody2D hook;
    public GameObject hookObject;
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
    private float timeTrailRenderer = 3.0f;

    private void Start()
    {
        StopBall();
    }

    private void Update()
    {
        if (isMove)
        {
          //  if ((rb.position-hook.position).normalized.magnitude <= 1.0f)
            GetComponent<CircleCollider2D>().enabled = true;

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
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;

        // this.enabled = false;
        isMove = true;
        timer = timeToGrab;
        readyText.color = Color.black;
        

        /*    yield return new WaitForSeconds(2f);

            if (nextBall != null)
            {
                nextBall.SetActive(true);
            }
            else
            {
                Debug.Log("YOU LOST");
                Enemy.EnemiesAlive = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }*/
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

    private void StopBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        hook.transform.position = transform.position;
        GetComponent<SpringJoint2D>().enabled = true;
        readyText.color = Color.green;
   //     tr.enabled = false;
        isMove = false;
    }

}
