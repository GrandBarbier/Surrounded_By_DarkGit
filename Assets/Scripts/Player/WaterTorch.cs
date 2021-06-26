using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTorch : MonoBehaviour
{
    public bool canBePicked;
    public GameObject t_light;
    public GameObject particles;
    public AudioClip torch_extinction;
    public new AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            Debug.Log("PUTE");
            canBePicked = true;
        }
        else if (other.gameObject.CompareTag("water"))
        {
            if (t_light.activeSelf)
            {
                audio.Stop();
                audio.clip = torch_extinction;
                audio.Play();
            }
            t_light.SetActive(false);
            particles.SetActive(false);
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            Debug.Log("PAS PUTE");
            canBePicked = false;
        }
    }
}
