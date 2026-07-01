using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetPlayerPrefs : MonoBehaviour
{
    public GameObject copy;
    public void TestButton()
    {
#if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
#endif
    }

    public void CopyRight()
    {
        copy.SetActive(!copy.activeSelf);
    }
}
