using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestionBGM : MonoBehaviour
{
    public GameObject music_on;
    public GameObject music_off1;
    public GameObject music_off2;
    public GameObject music_off3;
    private float valeur_on;

    private void Start()
    {
        valeur_on = 0.3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            music_on.GetComponent<AudioSource>().volume = valeur_on;
            music_off1.GetComponent<AudioSource>().volume = 0f;
            music_off2.GetComponent<AudioSource>().volume = 0f;
            music_off3.GetComponent<AudioSource>().volume = 0f;

        }
    }
}
