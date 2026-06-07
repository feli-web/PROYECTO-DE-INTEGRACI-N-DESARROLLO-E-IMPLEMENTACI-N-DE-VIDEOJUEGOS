using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarsAndHearts : MonoBehaviour
{
    public int stars;
    public int hearts;
    public TextMeshProUGUI heartAmount;
    public TextMeshProUGUI starAmount;
    void Start()
    {
        stars = PlayerPrefs.GetInt("StarAmount", 0);
        starAmount. text = stars.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
