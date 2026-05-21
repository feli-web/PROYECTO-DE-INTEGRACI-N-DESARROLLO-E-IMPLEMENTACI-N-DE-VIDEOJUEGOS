using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
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
                hit--;
                if (hit <= 0)
                {
                    Destroy(gameObject);
                }
        }
    }
}
