using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Gift : MonoBehaviour
{
    public TextMeshPro text;
    public GiftManager giftManager;
    public Transform or;
    public Transform deer;
    public int dangerMometr = 5;

    private bool isReady = false;
    private Transform lastParent;
    private int giftId;
    private int clickCount = 0;

    private bool hasTerroristMode = false;
    private Treasure treasure;

    private void Start()
    {
        lastParent = transform.parent;
    }

    public void Unpack (Treasure newTreasure) {
        treasure = newTreasure;
        text.text = newTreasure.msg;
        giftId = newTreasure.id;

        or.gameObject.SetActive(newTreasure.oranges > 0);
        deer.gameObject.SetActive(newTreasure.reindeers > 0);

        if (newTreasure.bombs > 0)
            hasTerroristMode = true;
    }

    private void Update()
    {
        text.transform.LookAt(Camera.main.transform.position);
        
        if (clickCount == 3)
        {
            if (treasure.bombs > 0)
            {
                BombField.instance.Init(this, treasure);
                clickCount++;
            }
            else
            {
                GamePreferences.instance.oranges += treasure.oranges;
                GamePreferences.instance.reindeers += treasure.reindeers;
                GamePreferences.instance.SaveData();

                //FindObjectOfType<Vuforia.VuforiaBehaviour>().enabled = true;
                Destroy(gameObject);

                var addTreasure = new Treasure
                {
                    oranges = treasure.oranges,
                    reindeers = treasure.reindeers,
                    bombs = 1 + 2 * treasure.bombs
                };
                AddManager.isntance.InitWatchAdd(addTreasure);
            }
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
