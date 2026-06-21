using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour,IUnityAdsInitializationListener
{
    [SerializeField] private string androidgameId;
    [SerializeField] private bool isTesting;
    private string gameId;

    // Start is called before the first frame update
    void Awake()
    {
        gameId = androidgameId;

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTesting, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Failure");
    }
}

