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

            // Only read headers
            if (!line.StartsWith("LV"))
                continue;

            // Example:
            // LV1|20

            string[] data = line.Split('|');

            if (data.Length < 2)
                continue;

            LevelData level = new LevelData();

            // LEVEL NUMBER
            string levelString = data[0].Replace("LV", "");
            level.levelNumber = int.Parse(levelString);

            // BULLETS
            level.bullets = int.Parse(data[1]);

            levels.Add(level);
        }

        // SORT LOWEST TO HIGHEST
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

            // LV1 always unlocked
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

            // STARS
            Transform star1 =
                buttonObj.transform.Find("Stars/Star1");

            Transform star2 =
                buttonObj.transform.Find("Stars/Star2");

            Transform star3 =
                buttonObj.transform.Find("Stars/Star3");

            if (star1 != null)
                star1.gameObject.SetActive(stars >= 1);

            if (star2 != null)
                star2.gameObject.SetActive(stars >= 2);

            if (star3 != null)
                star3.gameObject.SetActive(stars >= 3);

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

        // SAVE CURRENT LEVEL
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);

        // Example:
        SceneManager.LoadScene(1);
    }
}