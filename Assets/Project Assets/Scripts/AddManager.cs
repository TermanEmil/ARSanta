using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AddManager : MonoBehaviour
{
    public static AddManager isntance;

    public GameObject watchAddPanel;
    public TextMeshProUGUI oranges;
    public TextMeshProUGUI reindeers;
    public TextMeshProUGUI bombs;

    public GameObject thanksForWatching;
    public TextMeshProUGUI orangesWin;
    public TextMeshProUGUI reindeersWin;
    public TextMeshProUGUI bombsWin;

    private Treasure treasure;

    private void Awake()
    {
        isntance = this;
    }

    private void Start()
    {
        End();
    }

    public void InitWatchAdd(Treasure treasure)
    {
        this.treasure = treasure;
        oranges.text = treasure.oranges.ToString();
        reindeers.text = treasure.reindeers.ToString();
        bombs.text = treasure.bombs.ToString();
        watchAddPanel.SetActive(true);
    }

    public void PlayAdd()
    {
        watchAddPanel.SetActive(false);
        ShowRewardedAd();
    }

    public void DontWatchAdd()
    {
        End();
    }

    public void FinishVideoWatch()
    {
        watchAddPanel.SetActive(true);
        GamePreferences.instance.oranges += treasure.oranges;
        GamePreferences.instance.reindeers += treasure.reindeers;
        GamePreferences.instance.bombs += treasure.bombs;
        GamePreferences.instance.SaveData();
        End();
    }

    public void End()
    {
        watchAddPanel.SetActive(false);
        thanksForWatching.SetActive(false);
        FindObjectOfType<Vuforia.VuforiaBehaviour>().enabled = true;
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                orangesWin.text = treasure.oranges.ToString();
                reindeersWin.text = treasure.reindeers.ToString();
                bombsWin.text = treasure.bombs.ToString();
                thanksForWatching.SetActive(true);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                End();
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                End();
                break;
        }
    }
}
