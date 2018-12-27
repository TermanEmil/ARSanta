using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class Treasure
{
    public int id;
    public string author;
    public string msg;
    public string modelName;
    public int oranges = 0;
    public int reindeers = 0;
    public int bombs = 0;
}

// These weird class is needed.
[Serializable]
public class TreasuresResponse
{
    public List<Treasure> treasures;
}

public class TreasureAddModel
{
    public string author;
    public string msg;
    public string model_image_name;
    public int oranges = 0;
    public int reindeers = 0;
    public int bombs = 0;
}

public class GiftManager : MonoBehaviour
{
    public static string baseUrl;

    public GameObject giftPrefab1;
    public float updateRate = 2;
    public GameObject convas;
    public GameObject convasSucceed;
    public Text succeedText;
    public InputField text;

    public Transform pos1;
    public Transform[] listOfPoints;
    private List<Gift> gifts = new List<Gift>();

    private void Awake()
    {
        baseUrl = string.Format("http://{0}/santa/", GamePreferences.instance.serverUrl);
    }

    private void Start()
    {
        foreach (var pos in listOfPoints)
        {
            var gift = Instantiate(giftPrefab1, pos) as GameObject;
            gift.transform.localPosition = Vector3.zero;
            gift.transform.localRotation = Quaternion.identity;

            var giftComponent = gift.GetComponent<Gift>();
            giftComponent.giftManager = this;

            gifts.Add(giftComponent);
            gift.SetActive(false);
        }

        StartCoroutine("LoadTreasureForModel", gameObject.name);
    }

    IEnumerator LoadTreasureForModel(string modelName)
    {
        var url = baseUrl + "get_treasures_on_model/" + modelName;
        using (var www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Accept", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var treasuresResponse = JsonUtility.FromJson<TreasuresResponse>(www.downloadHandler.text);
                var treasures = treasuresResponse.treasures;

                var i = 0;
                foreach (var treasure in treasures)
                {
                    if (i >= gifts.Count)
                        break;

                    var gift = gifts[i];
                    gift.Unpack(treasure);
                    gift.gameObject.SetActive(true);
                    i++;
                }

                for (; i < gifts.Count; i++)
                {
                    gifts[i].gameObject.SetActive(false);
                }
            }
        }

        yield return new WaitForSeconds(2);
        StartCoroutine("LoadTreasureForModel", gameObject.name);
    }

    public void OpenAddGift ()
    {
        GamePreferences.instance.currentGiftManager = this;
        convas.SetActive(true);
        convasSucceed.SetActive(false); // temporary
    }

    public void CancelAddGift ()
    {
        text.text = "";

        convas.SetActive(false);
    }

    public void AddGift()
    {
        StartCoroutine("AddGiftRequest");
        // add gift on server

        //convasSucceed.SetActive(true);
        //convasSucceed.gameObject.GetComponent<Animator>().SetTrigger("fade");
        //convas.SetActive(false);
    }

    private IEnumerator AddGiftRequest()
    {
        print(gameObject.name + "!!!");

        var model = new TreasureAddModel
        {
            author = "Unknown",
            msg = text.text,
            model_image_name = gameObject.name,
            oranges = GamePreferences.instance.orangesInGift,
            reindeers = GamePreferences.instance.reindeersInGift,
            bombs = GamePreferences.instance.bombsInGift
        };

        var modelJson = JsonUtility.ToJson(model);
        var bytes = Encoding.ASCII.GetBytes(modelJson);
        var uploadHandler = new UploadHandlerRaw(bytes);
        uploadHandler.contentType = "application/json";

        var url = baseUrl + "add_treasure";

        var www = UnityWebRequest.Post(url, modelJson);
        www.uploadHandler = uploadHandler;

        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            succeedText.text = "Failed to add";
        }
        else
        {
            succeedText.text = "Success";
        }

        convasSucceed.SetActive(true);
        convasSucceed.gameObject.GetComponent<Animator>().SetTrigger("fade");
        convas.SetActive(false);
    }

    public void RemoveGift(Gift targetGift)
    {
        gifts.Remove(targetGift);
        
        foreach (var pos in listOfPoints)
        {
            if (pos.childCount == 0)
            {
                var gift = Instantiate(giftPrefab1, pos) as GameObject;
                gift.transform.localPosition = Vector3.zero;
                gift.transform.localRotation = Quaternion.identity;

                var giftComponent = gift.GetComponent<Gift>();
                giftComponent.giftManager = this;

                gifts.Add(giftComponent);
                gift.SetActive(false);
            }
        }
    }
}
