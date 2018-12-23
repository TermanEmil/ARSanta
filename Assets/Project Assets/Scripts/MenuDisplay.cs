using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuDisplay : MonoBehaviour
{
    public TextMeshProUGUI orangesText;
    public TextMeshProUGUI reindeersText;
    public TextMeshProUGUI bombsText;

    private void Start()
    {
        orangesText.text = PlayerPrefs.GetInt("oranges", 0).ToString();
        reindeersText.text = PlayerPrefs.GetInt("reindeers", 0).ToString();
        bombsText.text = PlayerPrefs.GetInt("bombs", 0).ToString();
    }

    public void Play ()
    {
        SceneManager.LoadScene("Main2");
    }
}
