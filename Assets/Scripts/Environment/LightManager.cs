using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private GameObject target;
    public GameObject pointLight;
  //  public GameObject newBallSprite;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        pointLight.transform.SetParent(target.transform);
   //     newBallSprite.transform.SetParent(target.transform);
        
    }
}
