using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTorch : MonoBehaviour
{
    public bool canBePicked;
    public GameObject t_light;
    public GameObject particles;
    public new AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            canBePicked = true;
        }
        else if (other.gameObject.CompareTag("water"))
        {
            t_light.SetActive(false);
            particles.SetActive(false);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            canBePicked = false;
        }
    }
}
