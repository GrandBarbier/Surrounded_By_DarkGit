using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firststepbutton : MonoBehaviour
{
    public GameObject firstPartLights;
    public GameObject secondPartLights;
    public bool neverused;
    // Start is called before the first frame update

    void Start()
    {
        firstPartLights.SetActive(true);
        secondPartLights.SetActive(false);
    }


    void Update()

    {

        if (neverused)
        {
            neverused = false;
            firstPartLights.SetActive(false);
            secondPartLights.SetActive(true);
            //trigger sound
            //trigger animation (ou particle system
        }
    }
    

}
