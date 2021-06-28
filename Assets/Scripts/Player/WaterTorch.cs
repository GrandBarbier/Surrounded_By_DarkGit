using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTorch : MonoBehaviour
{
    public bool canBePicked;
    public GameObject t_light;
    public GameObject particles;
    public GameObject SFX;

    public bool torchOff;
    public bool torchOffLastFrame;
    
    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
    }

    private void Update()
    {
        if (!torchOffLastFrame)
        {
            if (torchOff)
            {
                t_light.SetActive(false);
                particles.SetActive(false);
                SFX.SetActive(false);
                //jouer extinction
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
                instance.start();
            }
        }
        torchOffLastFrame = torchOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            canBePicked = true;
        }
        else if (other.gameObject.CompareTag("water"))
        {
            torchOff = true;
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
