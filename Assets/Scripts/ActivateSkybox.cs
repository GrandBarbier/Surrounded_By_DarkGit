using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSkybox : MonoBehaviour
{
    public Camera cam;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        cam.clearFlags = CameraClearFlags.Skybox;
    }
}
