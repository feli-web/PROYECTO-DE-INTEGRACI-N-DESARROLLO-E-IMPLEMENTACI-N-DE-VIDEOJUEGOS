using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerPuzzleShoot : MonoBehaviour
{
    LevelTextCreator levelTextCreator;

    int stars;
    int startingBullets;
    int remainingBullets;
    public AudioClip clickSound;

    [Header("Star Display")]
    public Image starDisplayImage;
    public Sprite[] starSprites;

    void Start()
    {
        levelTextCreator = GameObject.Find("LevelTextCreator").GetComponent<LevelTextCreator>();
        startingBullets = levelTextCreator.bullets;
        StarDisplay();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WinCondition()
    {
        StarCalc();

        levelTextCreator.SaveLevelProgress(true, stars);
        
        SceneManager.LoadScene(2);
        PlayerPrefs.SetString("YesNo", "YES");
    }
    public void LoseCondition()
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetString("YesNo", "NO");
    }

    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
        AudioManager.Instance.PlaySFX(clickSound);
    }
    public void StarDisplay()
    {
        stars = levelTextCreator.stars;
        starDisplayImage.sprite = starSprites[stars];
    }
    public void StarCalc()
    {
        remainingBullets = GameObject.Find("Cannon").GetComponent<Cannon>().numberOfShots;
        float percentage = (float)remainingBullets / startingBullets;

        if (percentage >= 0.66f)
            stars = 3;
        else if (percentage >= 0.33f)
            stars = 2;
        else if (percentage > 0f)
            stars = 1;
        else
            stars = 0;
    }

    public void BGMButton()
    {
        AudioManager.Instance.bgmSource.mute = !AudioManager.Instance.bgmSource.mute;
    }
    public void SFXButton()
    {
        AudioManager.Instance.sfxSource.mute = !AudioManager.Instance.sfxSource.mute;
    }
}
