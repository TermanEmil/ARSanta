﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Awake()
    {
        StopCoroutine("LoadTreasureForModel");
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
                    if (i >= listOfPoints.Length)
                        break;

                    var gift = Instantiate(giftPrefab1, listOfPoints[i]) as GameObject;
                    gift.transform.localPosition = Vector3.zero;
                    gift.transform.localRotation = Quaternion.identity;

                    var giftComponent = gift.GetComponent<Gift>();
                    giftComponent.Unpack(treasure);

                    i++;
                }
            }
        }
    }
}