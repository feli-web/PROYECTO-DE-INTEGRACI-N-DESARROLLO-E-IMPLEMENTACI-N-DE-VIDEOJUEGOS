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

    [Header("Star Display")]
    public Sprite[] starSprites; // 0 stars, 1 star, 2 stars, 3 stars

    private List<LevelData> levels = new List<LevelData>();

    void Start()
    {
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

        foreach (LevelData level in levels)
        {
            GameObject buttonObj =
                Instantiate(buttonPrefab, contentParent);

            // BUTTON
            Button button =
                buttonObj.GetComponent<Button>();

            // TEXT
            TMP_Text levelText =
                buttonObj.GetComponentInChildren<TMP_Text>();

            levelText.text = level.levelNumber.ToString();

            // LOAD SAVE DATA
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

            // LOCK SYSTEM
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

            // LOCK ICON
            Transform lockIcon =
                buttonObj.transform.Find("LockIcon");

            if (lockIcon != null)
            {
                lockIcon.gameObject.SetActive(locked);
            }

            // BUTTON INTERACTABLE
            button.interactable = !locked;

            // STAR DISPLAY IMAGE
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

            // CLICK EVENT
            int levelNumberCopy = level.levelNumber;

            button.onClick.AddListener(() =>
            {
                EnterLevel(levelNumberCopy);
            });
        }
    }

    public void EnterLevel(int levelNumber)
    {
        Debug.Log("Entering Level: " + levelNumber);

        PlayerPrefs.SetInt("CurrentLevel", levelNumber);

        SceneManager.LoadScene(1);
    }
}