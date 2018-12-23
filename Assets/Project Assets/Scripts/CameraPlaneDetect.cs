using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaneDetect : MonoBehaviour
{
    public GameObject partcl;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Platform")
            partcl.SetActive(true);

        print("Active");
    }

    private void OnTriggerExit(Collider other)
    {
        // (other.tag == "Platform")
            partcl.SetActive(false);

        print("Inactive");
    }
}
