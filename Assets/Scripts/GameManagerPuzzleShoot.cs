using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerPuzzleShoot : MonoBehaviour
{
    LevelTextCreator levelTextCreator;

    int stars;
    int startingBullets;
    int remainingBullets;

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

        SceneManager.LoadScene(0);
    }
    public void LoseCondition()
    {
        Debug.Log("You Lost");
    }

    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
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
}
