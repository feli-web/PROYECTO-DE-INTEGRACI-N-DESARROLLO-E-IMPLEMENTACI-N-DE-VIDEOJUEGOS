using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    GameObject key;
    public AudioClip unlockSound;
    public float dissappearTime;
    void Start()
    {
        key = GameObject.FindWithTag("Key");
    }

    // Update is called once per frame
    void Update()
    {
        if (key == null)
        {
            AudioManager.Instance.PlaySFX(unlockSound);
            Destroy(gameObject);
        }    
    }
    
}
