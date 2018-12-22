using System;
using System.Collections;
using System.Collections.Generic;
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
}

// These weird class is needed.
[Serializable]
public class TreasuresResponse
{
    public List<Treasure> treasures;
}

public class GiftManager : MonoBehaviour
{
    private static string baseUrl = "http://172.31.199.77:8000/santa/";

    public GameObject giftPrefab1;
    public float updateRate = 2;
    public GameObject convas;
    public GameObject convasSucceed;
    public InputField text;

    public Transform pos1;
    public Transform[] listOfPoints;
    private List<Gift> gifts = new List<Gift>();

    private void Start()
    {
        foreach (var pos in listOfPoints)
        {
            var gift = Instantiate(giftPrefab1, pos) as GameObject;
            gift.transform.localPosition = Vector3.zero;
            gift.transform.localRotation = Quaternion.identity;

            gifts.Add(gift.GetComponent<Gift>());
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
        convas.SetActive(true);
        convasSucceed.SetActive(false); // temporary
    }

    public void CancelAddGift ()
    {
        text.text = "";

        convas.SetActive(false);
    }

    public void AddGift ()
    {
        // add gift on server

        convasSucceed.SetActive(true);
        convasSucceed.gameObject.GetComponent<Animator>().SetTrigger("fade");
        convas.SetActive(false);
    }
}
