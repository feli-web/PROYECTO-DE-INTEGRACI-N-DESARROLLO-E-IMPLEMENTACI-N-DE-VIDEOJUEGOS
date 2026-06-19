using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FinalStar : MonoBehaviour
{
    public float rotateSpeed;
    public AudioClip starClip;
    public float dissappearTime;
    void Start()
    {
        transform.DORotate(
    new Vector3(0, 0, 360),
    rotateSpeed,
    RotateMode.FastBeyond360)
    .SetLoops(-1, LoopType.Incremental)
    .SetEase(Ease.Linear);
    }

    // Update is called once per frame
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameManagerPuzzleShoot gm = GameObject.FindWithTag("Manager").GetComponent<GameManagerPuzzleShoot>();
            AudioManager.Instance.PlaySFX(starClip);
            Destroy(collision.gameObject);
            transform.DOScale(Vector3.zero, dissappearTime).OnComplete(() => gm.WinCondition());
            Invoke("WC",dissappearTime);
        }
    }
}
