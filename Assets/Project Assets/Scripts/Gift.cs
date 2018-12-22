using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gift : MonoBehaviour
{
    public TextMeshPro text;

    private bool isReady = false;

    public void Unpack (Treasure newTreasure) {
        text.text = newTreasure.msg;
    }

    private void Update()
    {
        text.transform.LookAt(Camera.main.transform.position);

        //transform.LookAt(Camera.main.transform.position);
    }

    public void Open()
    {
        if (isReady)
            GetComponent<Animator>().SetTrigger("open");
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
        transform.localRotation = Quaternion.identity;

        //Camera.main.gameObject.GetComponent<VuforiaMonoBehaviour>().enabled = false;

        isReady = true;

        FindObjectOfType<Vuforia.VuforiaBehaviour>().enabled = false;
    }
}
