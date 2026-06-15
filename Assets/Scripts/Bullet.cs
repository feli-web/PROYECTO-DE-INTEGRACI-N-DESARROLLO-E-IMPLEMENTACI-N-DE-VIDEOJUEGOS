using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Cannon cannon;
    public AudioClip bounceClip;
    void Start()
    {
        cannon = GameObject.FindWithTag("Player").GetComponent<Cannon>();
        Invoke("Life", 10f);
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
            cannon.canShoot = true;
            if (cannon.numberOfShots <= 0)
            {
                GameManagerPuzzleShoot gm = GameObject.FindWithTag("Manager").GetComponent<GameManagerPuzzleShoot>();
                gm.LoseCondition();
            }
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            AudioManager.Instance.PlaySFX(bounceClip);
        }
    }
    void Life()
    {
        Destroy(gameObject);
        cannon.canShoot=true;
    }
}
