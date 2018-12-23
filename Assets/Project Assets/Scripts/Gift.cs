using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Gift : MonoBehaviour
{
    public TextMeshPro text;
    public GiftManager giftManager;

    private bool isReady = false;
    private Transform lastParent;
    private int giftId;
    private int clickCount = 0;

    private void Start()
    {
        lastParent = transform.parent;
    }

    public void Unpack (Treasure newTreasure) {
        text.text = newTreasure.msg;
        giftId = newTreasure.id;
    }

    private void Update()
    {
        text.transform.LookAt(Camera.main.transform.position);

        if (clickCount >= 3)
        {
            FindObjectOfType<Vuforia.VuforiaBehaviour>().enabled = true;

            Destroy(gameObject);
        }
    }

    public void Open()
    {
        if (isReady)
        {
            GetComponent<Animator>().SetTrigger("open");
            StartCoroutine("RemoveGift");
        }
    }

    public void ClickInc ()
    {
        clickCount++;
    }

    public void Preview ()
    {
        Transform prevPos = GameObject.Find("gif_recieve_pos").transform;

        foreach (var component in Camera.main.GetComponents<Component>())
        {
            Debug.Log(component.name);
        }

        transform.position = prevPos.position;
        transform.SetParent(prevPos);
        giftManager.RemoveGift(this);
        transform.localRotation = Quaternion.identity;

        isReady = true;

        FindObjectOfType<Vuforia.VuforiaBehaviour>().enabled = false;
    }

    private IEnumerator RemoveGift()
    {
        var url = GiftManager.baseUrl + "remove_treasure/" + giftId;
        using (var www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Accept", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError || www.responseCode != 200)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Removed gift");
            }
        }
    }
}
