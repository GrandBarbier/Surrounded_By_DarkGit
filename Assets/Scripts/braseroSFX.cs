using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class braseroSFX : MonoBehaviour
{
    public GameObject t_light;
    public GameObject particles;
    public AudioClip braseroSfxClip;
    public new AudioSource audio;

    void Update()
    {
        if (!audio.isPlaying && t_light.activeSelf && particles.activeSelf)
        {
            audio.clip = braseroSfxClip;
            audio.Play();
        }
    }
}
