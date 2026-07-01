using DG.Tweening;
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip unlockSound;
    public float dissappearTime;
    public SpriteRenderer sr;
    private void Start()
    {
        sr.DOFade(0.5f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            {
            DestroyAllWithTag("Lock");
        }
    }

    public void DestroyAllWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        AudioManager.Instance.PlaySFX(unlockSound);
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Collider2D>().enabled = false;
            obj.GetComponent<SpriteRenderer>().DOFade(0, dissappearTime).OnComplete(() => Destroy(obj));
        }
    }

}
