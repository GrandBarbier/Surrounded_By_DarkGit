using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchSFX : MonoBehaviour
{
    public GameObject light;
    public GameObject particles;
    public AudioSource audio;
    public AudioClip torch_sfx;
    
    // Update is called once per frame
    void Update()
    {
        if (particles.activeSelf && light.activeSelf)
        {
            if (!audio.isPlaying)
            {
                audio.clip = torch_sfx;
                audio.Play();
            }
        }
    }
}
