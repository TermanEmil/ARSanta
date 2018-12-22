using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour
{
    public GameObject giftPrefab1;

    public Transform pos1;

    public void CreateGift () {
        GameObject newGift = Instantiate (giftPrefab1, transform);
        newGift.transform.position = pos1.position;
        // newGift.SetParent (transform);
    }
}
