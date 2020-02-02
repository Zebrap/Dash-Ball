using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startposX, startposY;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
      //  length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // parallax move
        float distY = (cam.transform.position.y * parallaxEffect);
        float distX = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        // repeating background
        /*
        float tempX = (cam.transform.position.x * (1 - parallaxEffect));
        float tempY = (cam.transform.position.y * (1 - parallaxEffect));
        if (tempY > startpos + length) startpos += length;
        else if (tempY < startpos - length) startpos -= length;*/
    }
}
