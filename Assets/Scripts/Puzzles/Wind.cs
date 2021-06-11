using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Collider[] windColliders;
    public ParticleSystem[] windParticles;

    public float gustDuration = 2f;
    public float timeBetweenGust = 5f;
    
    void Start()
    {
        StartCoroutine(WaitForGust());
    }
    
    void Update()
    {
        
    }

    public IEnumerator WaitForGust()
    {
        //Debug.Log("StartWaitForGust");
        //Debug.LogWarning("StartWaitForGust");
        
        yield return new WaitForSeconds(timeBetweenGust);
        
        //Debug.Log("StartGust");
        //Debug.LogWarning("StartGust");

        for (int i = 0; i < windColliders.Length; i++)
        {
            windColliders[i].gameObject.SetActive(true);
        }
        
        for (int i = 0; i < windParticles.Length; i++)
        {
            windParticles[i].Play();
        }
        
        yield return new WaitForSeconds(gustDuration);
        
        //Debug.Log("EndGustRestart");
        //Debug.LogWarning("EndGust_Restart");
        
        for (int i = 0; i < windColliders.Length; i++)
        {
            windColliders[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < windParticles.Length; i++)
        {
            windParticles[i].Stop();
        }

        StartCoroutine(WaitForGust());
    }
}
