using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource bgmSource;

    private Button bgmButton;
    private Button sfxButton;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void LoadSettings()
    {
        bgmSource.mute = PlayerPrefs.GetInt("BGMMuted", 0) == 1;
        sfxSource.mute = PlayerPrefs.GetInt("SFXMuted", 0) == 1;
    }

    private void UpdateButtonColors()
    {
        if (bgmButton != null)
        {
            bgmButton.image.color = bgmSource.mute ? Color.black : Color.white;
        }

        if (sfxButton != null)
        {
            sfxButton.image.color = sfxSource.mute ? Color.black : Color.white;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bgmButton = GameObject.Find("BGMButton")?.GetComponent<Button>();
        sfxButton = GameObject.Find("SFXButton")?.GetComponent<Button>();

        if (bgmButton != null)
        {
            bgmButton.onClick.RemoveListener(BGMButton);
            bgmButton.onClick.AddListener(BGMButton);
        }

        if (sfxButton != null)
        {
            sfxButton.onClick.RemoveListener(SFXButton);
            sfxButton.onClick.AddListener(SFXButton);
        }

        Invoke("UpdateButtonColors",0.1f);
    }

    public void PlaySFX(AudioClip clip)
    {
        float i = Random.Range(0.5f, 1f);
        sfxSource.PlayOneShot(clip);
        sfxSource.pitch = i;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    private void BGMButton()
    {
        bgmSource.mute = !bgmSource.mute;

        PlayerPrefs.SetInt("BGMMuted", bgmSource.mute ? 1 : 0);

        UpdateButtonColors();
    }

    private void SFXButton()
    {
        sfxSource.mute = !sfxSource.mute;

        PlayerPrefs.SetInt("SFXMuted", sfxSource.mute ? 1 : 0);

        UpdateButtonColors();
    }
}