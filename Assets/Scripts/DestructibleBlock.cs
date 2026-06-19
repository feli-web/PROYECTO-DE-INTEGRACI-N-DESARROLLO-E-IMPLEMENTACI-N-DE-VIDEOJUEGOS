using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    public AudioClip hitSound;
    public int hit;
    public float dissappearTime;
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
                GetComponent<Collider2D>().enabled = false;
                transform.DOScale(new Vector3(3, 3, 0), dissappearTime);
                GetComponent<SpriteRenderer>().DOFade(0, dissappearTime).OnComplete(() => Destroy(gameObject));
            }
        }
    }
}
