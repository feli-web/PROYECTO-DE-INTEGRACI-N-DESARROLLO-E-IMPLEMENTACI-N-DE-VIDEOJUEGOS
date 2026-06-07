using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterLevel : MonoBehaviour
{
    public GameObject WinUI;
    public GameObject LoseUI;
    public int currentLevel;
    string yesNo;
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        yesNo = PlayerPrefs.GetString("YesNo");
        if (yesNo == "YES")
        {
            WinUI.SetActive(true);
            LoseUI.SetActive(false);
        }
        else
        {
            WinUI.SetActive(false);
            LoseUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void NextLevel()
    {
        Debug.Log("Entering Level: " + currentLevel+1);

        PlayerPrefs.SetInt("CurrentLevel", currentLevel+1);

        SceneManager.LoadScene(1);
    }

}
