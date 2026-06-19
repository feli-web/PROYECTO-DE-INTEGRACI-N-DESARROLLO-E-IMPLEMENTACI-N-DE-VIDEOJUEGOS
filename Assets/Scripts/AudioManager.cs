using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource bgmSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {;
        sfxSource.PlayOneShot(clip);
    }
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip =clip;
        bgmSource.Play();
    }
}