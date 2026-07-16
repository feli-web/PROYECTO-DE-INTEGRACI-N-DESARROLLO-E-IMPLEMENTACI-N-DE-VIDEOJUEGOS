using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public InitializeAds inital;
    public nterstitialAds inter;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

        //inter.LoadInterstitialAd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
