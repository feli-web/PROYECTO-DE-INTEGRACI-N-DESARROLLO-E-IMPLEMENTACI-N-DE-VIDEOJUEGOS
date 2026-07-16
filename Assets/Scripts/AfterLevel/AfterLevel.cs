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
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        yesNo = PlayerPrefs.GetString("YesNo");
        if (yesNo == "YES")
        {
            WinUI.SetActive(true);
            LoseUI.SetActive(false);
            AudioManager.Instance.PlaySFX(winSound);
        }
        else
        {
            WinUI.SetActive(false);
            LoseUI.SetActive(true);
            AudioManager.Instance.PlaySFX(loseSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
        AudioManager.Instance.PlaySFX(clickSound);
        if (i == 1)
        {
            if (PlayerPrefs.GetInt("AdShowCount") >= 2)
            {
                AdsManager.Instance.inter.ShowInterstitialAd();
                PlayerPrefs.SetInt("AdShowCount", 0);
            }
        }
    }
    public void NextLevel()
    {
        Debug.Log("Entering Level: " + currentLevel+1);

        PlayerPrefs.SetInt("CurrentLevel", currentLevel+1);
        AudioManager.Instance.PlaySFX(clickSound);
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("AdShowCount", PlayerPrefs.GetInt("AdShowCount") + 1);
    }


}
