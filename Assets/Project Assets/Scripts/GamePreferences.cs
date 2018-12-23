using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePreferences : MonoBehaviour
{
    public static GamePreferences instance;

    public int oranges;
    public int reindeers;
    public int bombs;

    public int orangesInGift;
    public int reindeersInGift;
    public int bombsInGift;

    public TextMeshProUGUI orangesText;
    public TextMeshProUGUI reindeersText;
    public TextMeshProUGUI bombsText;

    public TextMeshProUGUI orangesTextInGift;
    public TextMeshProUGUI reindeersTextInGift;
    public TextMeshProUGUI bombsTextInGift;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        oranges = PlayerPrefs.GetInt("oranges", 0);
        reindeers = PlayerPrefs.GetInt("reindeers", 0);
        bombs = PlayerPrefs.GetInt("bombs", 0);
    }

    private void Update()
    {
        orangesTextInGift.text = orangesInGift.ToString();
        reindeersTextInGift.text = reindeersInGift.ToString();
        bombsTextInGift.text = bombsInGift.ToString();

        orangesText.text = oranges.ToString();
        reindeersText.text = reindeers.ToString();
        bombsText.text = bombs.ToString();
    }

    public void AddOrange ()
    {
        if (oranges <= 0)
            return;

        oranges--;
        orangesInGift++;
    }

    public void AddReideers()
    {
        if (reindeers <= 0)
            return;

        reindeers--;
        reindeersInGift++;
    }

    public void AddBombs()
    {
        if (bombsInGift > 0 || bombs <= 0)
            return;

        bombs--;
        bombsInGift++;
    }

    public void SaveData ()
    {
        PlayerPrefs.SetInt("oranges", oranges);
        PlayerPrefs.SetInt("reindeers", reindeers);
        PlayerPrefs.SetInt("bombs", bombs);

        orangesInGift = 0;
        reindeersInGift = 0;
        bombsInGift = 0;
    }
}
