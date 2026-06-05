using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LevelTextCreator : MonoBehaviour
{
    [System.Serializable]
    public class PrefabMapping
    {
        public char symbol;
        public GameObject prefab;
    }

    [Header("Level File")]
    public TextAsset mapFile;

    [Header("Level To Load")]
    public int levelNumber;
    string levelName;
    public string endLevelLine = "END";

    [Header("Level Data")]
    public int bullets;
    public bool completed;
    [Range(0, 3)]
    public int stars;
    public TextMeshProUGUI levelText;

    [Header("Grid Settings")]
    public float xSpacing = 0.6f;
    public float ySpacing = -0.6f;
    public Vector3 startPosition = new Vector3(-2.4f, 3.9f, 0f);

    [Header("Prefab Mappings")]
    public List<PrefabMapping> prefabMappings;

    private Dictionary<char, GameObject> prefabDictionary;

    void Start()
    {
        levelNumber = PlayerPrefs.GetInt("CurrentLevel");
        levelName = "LV" + levelNumber.ToString();
        levelText.text = "LV " + levelNumber;

        BuildDictionary();

        LoadLevelProgress();
        GenerateMap();
    }

    void BuildDictionary()
    {
        prefabDictionary = new Dictionary<char, GameObject>();

        foreach (PrefabMapping mapping in prefabMappings)
        {
            if (!prefabDictionary.ContainsKey(mapping.symbol))
            {
                prefabDictionary.Add(mapping.symbol, mapping.prefab);
            }
        }
    }

    void GenerateMap()
    {
        if (mapFile == null)
        {
            Debug.LogError("No map file assigned!");
            return;
        }

        string[] lines = mapFile.text.Replace("\r", "").Split('\n');

        bool readingLevel = false;
        int currentRow = 0;

        foreach (string rawLine in lines)
        {
            string line = rawLine.TrimEnd();

            // SEARCH FOR LEVEL HEADER
            if (!readingLevel)
            {
                if (line.StartsWith(levelName))
                {
                    readingLevel = true;

                    // Example:
                    // LV1|20

                    string[] data = line.Split('|');

                    if (data.Length >= 2)
                    {
                        bullets = int.Parse(data[1]);

                        Debug.Log(
                            "Loaded Level Data -> " +
                            "Bullets: " + bullets
                        );
                    }
                    else
                    {
                        Debug.LogWarning("Level header format invalid!");
                    }

                    continue;
                }

                continue;
            }

            // STOP READING LEVEL
            if (line == endLevelLine)
            {
                Debug.Log("Finished loading level: " + levelName);
                break;
            }

            // READ MAP LINES
            for (int x = 0; x < line.Length; x++)
            {
                char symbol = line[x];

                // EMPTY SPACE
                if (symbol == ' ')
                    continue;

                // UNKNOWN SYMBOL
                if (!prefabDictionary.ContainsKey(symbol))
                    continue;

                Vector3 spawnPos = new Vector3(
                    startPosition.x + (x * xSpacing),
                    startPosition.y + (currentRow * ySpacing),
                    startPosition.z
                );

                Instantiate(
                    prefabDictionary[symbol],
                    spawnPos,
                    Quaternion.identity
                );
            }

            currentRow++;
        }

        if (!readingLevel)
        {
            Debug.LogError("Level not found: " + levelName);
        }
    }

    public void SaveLevelProgress(bool completedValue, int starsValue)
    {
        completed = completedValue;

        // Only overwrite if new stars are better
        if (starsValue > stars)
        {
            stars = starsValue;
        }

        // SAVE
        PlayerPrefs.SetInt(levelName + "_Completed", completed ? 1 : 0);
        PlayerPrefs.SetInt(levelName + "_Stars", stars);

        PlayerPrefs.Save();

        Debug.Log(
            "Saved Progress -> " +
            levelName +
            " Completed: " + completed +
            " Stars: " + stars
        );
    }

    public void LoadLevelProgress()
    {
        completed =
            PlayerPrefs.GetInt(levelName + "_Completed", 0) == 1;

        stars =
            PlayerPrefs.GetInt(levelName + "_Stars", 0);
    }
}