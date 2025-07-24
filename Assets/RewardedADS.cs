using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedADS : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // Start is called before the first frame update
    private string GAME_ID = "5233484";
    public RaceData raceData;
    private const string REWARDED_VIDEO_PLACEMENT_HIGH = "RewardedAndroid_Unity_High";
    private const string REWARDED_VIDEO_PLACEMENT_MEDIUM = "RewardedAndroid_Unity_Med";
    private const string REWARDED_VIDEO_PLACEMENT_LOW = "RewardedAndroid_Unity_Low";
    private bool testMode = false;
    public TMP_Text rewardAmountText;
    public TMP_Text rewardPanelText;
    public GameObject rewardPanel;
    public Animator animator;
    private int reward;
    public bool adLoaded;
    public bool adCompleted;
    public Button showAds;
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;
    public void Initialize()
    {
        Advertisement.Initialize(GAME_ID, testMode, this);
    }

    public void LoadRewardedAdHigh()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT_HIGH, this);
    }
    public void LoadRewardedAdMed()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT_MEDIUM, this);
    }
    public void LoadRewardedAdLow()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT_LOW, this);
    }

    public void ShowRewardedAdHigh()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT_HIGH, this);
    }
    public void ShowRewardedAdMedium()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT_MEDIUM, this);
    }
    public void ShowRewardedAdLow()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT_LOW, this);
    }

    void Start()
    {
        showAds.onClick.AddListener(ShowRewardedAdLow);


    }
    void Awake()
    {
        adLoaded = false;
        adCompleted = false;
        Initialize();
        StartCoroutine(LoadAds());

    }
    
    IEnumerator LoadAds()
    {
        yield return new WaitForSeconds(2f);
        LoadRewardedAdLow();
    }
    
    private void GenerateRewardCoins()
    {
        if(raceData != null)
        {
            if (raceData.currentOffer == 0)
            {
                reward = 100000;
            }
            else reward = raceData.currentOffer * 2;
            rewardAmountText.text = PRUtils.CurrencyFormater(reward.ToString());
            animator.SetTrigger("on");
        }
        else
        {
            userData = GameObject.Find("/UserData").GetComponent<userData>();
            if(userData.playerCoins<10000)
            {
                reward = 100000;
                rewardAmountText.text = PRUtils.CurrencyFormater(reward.ToString());
                animator.SetTrigger("on");
            }
        }

    }
    private userData userData;
    private void GiveReward()
    {
        if (raceData != null)
        {
            raceData.PlayerData.UserData.playerCoins += reward;
            animator.SetTrigger("off");
            raceData.PlayerData.UserData.WriteThenRead();
            raceData.PlayerData.UpdateUI();
            LoadRewardedAdLow();
        }
        else
        {

            animator.SetTrigger("off");
            player.AdReward(reward);

        }
    }
    public Player player;
    // Update is called once per frame
    public void OnGameIDFieldChanged(string newInput)
    {
        GAME_ID = newInput;
    }

    public void ToggleTestMode(bool isOn)
    {
        testMode = isOn;
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
        //LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
        Initialize();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
        adLoaded = true;
        GenerateRewardCoins();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
        adLoaded = false;
        //LoadRewardedAdLow();
        //LoadRewardedAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
        //ShowRewardedAd();
        rewardPanel.SetActive(true);
        animator.SetTrigger("off");
        rewardPanelText.text = "Reward Failed.";
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
        adCompleted = true;
        rewardPanel.SetActive(true);
        rewardPanelText.text = "You have got " + PRUtils.CurrencyFormater(reward.ToString()) +" Star Coins as Reward.";
        GiveReward();
    }
    #endregion
    //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}
