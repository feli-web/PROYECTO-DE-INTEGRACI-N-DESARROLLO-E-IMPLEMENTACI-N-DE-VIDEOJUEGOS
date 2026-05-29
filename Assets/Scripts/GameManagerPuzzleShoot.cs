using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPuzzleShoot : MonoBehaviour
{
    LevelTextCreator levelTextCreator;
    int stars;

    void Start()
    {
        levelTextCreator = GameObject.Find("LevelTextCreator").GetComponent<LevelTextCreator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WinCondition()
    {
        levelTextCreator.SaveLevelProgress(true, stars);
        SceneManager.LoadScene(0);
    }
    public void LoseCondition()
    {
        Debug.Log("You Lost");
    }
  

  
}
