using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    public AudioClip hitSound;
    public int hit;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Instance.PlaySFX(hitSound);
            hit--;
            if (hit <= 0)
                {
                    Destroy(gameObject);
                }
        }
    }
}
