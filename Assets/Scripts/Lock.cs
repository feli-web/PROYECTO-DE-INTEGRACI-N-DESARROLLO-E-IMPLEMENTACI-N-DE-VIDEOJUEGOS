using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    GameObject key;
    void Start()
    {
        key = GameObject.FindWithTag("Key");
    }

    // Update is called once per frame
    void Update()
    {
        if (key == null)
        {
            Destroy(gameObject);
        }    
    }
    
}
