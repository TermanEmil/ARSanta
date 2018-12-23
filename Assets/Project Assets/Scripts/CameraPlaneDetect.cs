using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaneDetect : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
        }

    // Do something with the object that was hit by the raycast.
}
