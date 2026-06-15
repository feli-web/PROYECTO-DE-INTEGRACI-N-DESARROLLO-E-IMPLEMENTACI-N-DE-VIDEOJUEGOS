using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalStar : MonoBehaviour
{
    public Vector3 rotateSpeed;
    public AudioClip starClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            GameManagerPuzzleShoot gm = GameObject.FindWithTag("Manager").GetComponent<GameManagerPuzzleShoot>();
            gm.WinCondition();
            AudioManager.Instance.PlaySFX(starClip);
        }
    }
}
