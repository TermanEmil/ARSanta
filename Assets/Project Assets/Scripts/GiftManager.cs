using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour
{
    public GameObject giftPrefab1;

    public Transform pos1;

    public void CreateGift () {
        Instantiate (giftPrefab1, pos1.position, Quaternion.identity);
    }
}
