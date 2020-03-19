using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoader : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.LoadScene("Menu");
    }
}
