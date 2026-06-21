using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelSelectorGenerator : MonoBehaviour
{
    [System.Serializable]
    public class LevelData
    {
        public int levelNumber;
        public int bullets;
    }

    [Header("TXT File")]
    public TextAsset levelFile;

    [Header("UI")]
    public Transform contentParent;
    public GameObject buttonPrefab;
    public ScrollRect scrollRect;

    [Header("Star Display")]
    public Sprite[] starSprites; 
    
    [Header("Sound")]
    public AudioClip clickSound; 
    public AudioClip bgm; 


    private List<LevelData> levels = new List<LevelData>();


    void Start()
    {
        AudioManager.Instance.PlayBGM(bgm);
        ReadLevels();
        GenerateButtons();
    }

    void ReadLevels()
    {
        string[] lines = levelFile.text.Replace("\r", "").Split('\n');

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (string.IsNullOrEmpty(line))
                continue;

            if (!line.StartsWith("LV"))
                continue;

            string[] data = line.Split('|');

            if (data.Length < 2)
                continue;

            LevelData level = new LevelData();

            string levelString = data[0].Replace("LV", "");
            level.levelNumber = int.Parse(levelString);

            level.bullets = int.Parse(data[1]);

            levels.Add(level);
        }

        levels.Sort((a, b) => a.levelNumber.CompareTo(b.levelNumber));
    }

    void GenerateButtons()
    {
        bool foundCurrentLevel = false;

        RectTransform highestUnlockedButton = null;

        foreach (LevelData level in levels)
        {
            GameObject buttonObj =
                Instantiate(buttonPrefab, contentParent);

            Button button =
                buttonObj.GetComponent<Button>();

            TMP_Text levelText =
                buttonObj.GetComponentInChildren<TMP_Text>();

            levelText.text = level.levelNumber.ToString();

            bool completed =
                PlayerPrefs.GetInt(
                    "LV" + level.levelNumber + "_Completed",
                    0
                ) == 1;

            int stars =
                PlayerPrefs.GetInt(
                    "LV" + level.levelNumber + "_Stars",
                    0
                );

            bool locked = false;

            if (level.levelNumber == 1)
            {
                locked = false;
            }
            else
            {
                if (!foundCurrentLevel)
                {
                    if (!completed)
                    {
                        foundCurrentLevel = true;
                    }
                }
                else
                {
                    locked = true;
                }
            }

            Transform lockIcon =
                buttonObj.transform.Find("LockIcon");

            if (lockIcon != null)
            {
                lockIcon.gameObject.SetActive(locked);
            }

            button.interactable = !locked;

            if (!locked)
            {
                highestUnlockedButton =
                    buttonObj.GetComponent<RectTransform>();
            }

            Transform starDisplayTransform =
                buttonObj.transform.Find("StarDisplay");

            if (starDisplayTransform != null)
            {
                Image starDisplayImage =
                    starDisplayTransform.GetComponent<Image>();

                if (starDisplayImage != null &&
                    starSprites != null &&
                    starSprites.Length > 0)
                {
                    int spriteIndex =
                        Mathf.Clamp(stars, 0, starSprites.Length - 1);

                    starDisplayImage.sprite =
                        starSprites[spriteIndex];
                }
            }

            int levelNumberCopy = level.levelNumber;

            button.onClick.AddListener(() =>
            {
                EnterLevel(levelNumberCopy);
            });
        }

        Canvas.ForceUpdateCanvases();

        if (highestUnlockedButton != null)
        {
            ScrollToButton(highestUnlockedButton);
        }
    }

    void ScrollToButton(RectTransform target)
    {
        if (scrollRect == null)
            return;

        Canvas.ForceUpdateCanvases();

        RectTransform contentRect =
            contentParent as RectTransform;

        float contentHeight =
            contentRect.rect.height;

        float viewportHeight =
            scrollRect.viewport.rect.height;

        float targetY =
            Mathf.Abs(target.anchoredPosition.y);

        float normalizedPosition =
            1f - (targetY / (contentHeight - viewportHeight));

        scrollRect.verticalNormalizedPosition =
            Mathf.Clamp01(normalizedPosition);
    }

    public void EnterLevel(int levelNumber)
    {
        Debug.Log("Entering Level: " + levelNumber);

        PlayerPrefs.SetInt("CurrentLevel", levelNumber);

        SceneManager.LoadScene(1);

        AudioManager.Instance.PlaySFX(clickSound);

        if (PlayerPrefs.GetInt("AdShowCount") >= 2)
        {
            AdsManager.Instance.inter.ShowInterstitialAd();
            PlayerPrefs.SetInt("AdShowCount", 0);
        }
    }

}