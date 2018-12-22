using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
    public TextMeshPro text;

    public Transform pos1;
    public Transform[] listOfPoints;

    public void CreateGift () {
        GameObject newGift = Instantiate (giftPrefab1, transform);
        newGift.transform.position = pos1.position;
        // newGift.SetParent (transform);

        StartCoroutine("LoadTreasureForModel", "P_1");
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

                //print (treasures.Count);
                var i = 0;
                foreach (var treasure in treasures) {
                    // if (listOfPoints.Length)
                    i++;
                }
            }
        }
    }
}
