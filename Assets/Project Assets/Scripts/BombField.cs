using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombField : MonoBehaviour
{
    public Transform bombObj;
    public float startProgress = 0.4f;
    public float minSize = 1;
    public float maxSize = 5;
    public float tapSpeed = 0.15f;
    public float regenSpeed = 0.11f;
    public float progress = 0;

    private Treasure treasure;
    private Gift gift;

    public GameObject explosion;

    public static BombField instance;

    private void Awake()
    {
        instance = this;
    }

    public void Init(Gift gift, Treasure treasure)
    {
        this.treasure = treasure;
        this.gift = gift;
        progress = startProgress;

        print("Init!");
    }

    private void Update()
    {
        if (treasure != null)
        {
            if (Input.GetMouseButtonDown(0))
                progress -= tapSpeed;

            progress += regenSpeed * Time.deltaTime;

            if (progress <= 0)
            {
                Win();
            }
            else if (progress >= 1)
            {
                Loose();
            }
            else
            {
                bombObj.localScale = Vector3.one * (minSize + (maxSize - minSize) * progress);
            }
        }
        else
            bombObj.localScale = Vector3.zero;
    }

    private void Win()
    {
        GamePreferences.instance.oranges += treasure.oranges;
        GamePreferences.instance.reindeers += treasure.reindeers;
        GamePreferences.instance.SaveData();

        var addTreasure = new Treasure
        {
            oranges = treasure.oranges,
            reindeers = treasure.reindeers,
            bombs = 1 + 2 * treasure.bombs
        };
        End();
        AddManager.isntance.InitWatchAdd(addTreasure);
    }

    private void Loose()
    {
        GamePreferences.instance.oranges -= treasure.oranges;
        GamePreferences.instance.reindeers -= treasure.reindeers;

        if (GamePreferences.instance.oranges < 0)
            GamePreferences.instance.oranges = 0;
        if (GamePreferences.instance.reindeers < 0)
            GamePreferences.instance.reindeers = 0;

        GamePreferences.instance.SaveData();

        var exp = Instantiate(explosion, bombObj.transform.position, Quaternion.identity) as GameObject;
        Destroy(exp, 2);

        bombObj.transform.localScale = Vector3.zero;

        Invoke("End", 2);
        treasure = null;
    }

    private void End()
    {
        treasure = null;
        //FindObjectOfType<Vuforia.VuforiaBehaviour>().enabled = true;
        Destroy(gift.gameObject);
    }
}
