using UnityEngine;
using System.Collections.Generic;

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

    [Header("Grid Settings")]
    public float xSpacing = 0.6f;
    public float ySpacing = -0.6f;
    public Vector3 startPosition = new Vector3(-2.4f, 3.9f, 0f);

    [Header("Prefab Mappings")]
    public List<PrefabMapping> prefabMappings;

    private Dictionary<char, GameObject> prefabDictionary;

    void Start()
    {
        levelName = "LV"+levelNumber.ToString();
        BuildDictionary();
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

            // Look for level name
            if (!readingLevel)
            {
                if (line == levelName)
                {
                    readingLevel = true;
                }

                continue;
            }

            // Stop reading level
            if (line == endLevelLine)
            {
                Debug.Log("Finished loading level: " + levelName);
                break;
            }

            // Read map line
            for (int x = 0; x < line.Length; x++)
            {
                char symbol = line[x];

                // SPACE = empty tile
                if (symbol == ' ')
                    continue;

                // Skip unknown symbols
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
}