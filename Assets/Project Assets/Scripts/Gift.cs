using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gift : MonoBehaviour
{
    public TextMeshPro text;

    public void Unpack (Treasure newTreasure) {
        text.text = newTreasure.msg;
    }
}
