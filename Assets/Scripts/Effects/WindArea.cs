using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public Vector3 direction;
    
    public float power;
    public float maxPower;
    public float time;

    private float actualTime;

    public bool ascending;
    public float ascendingTime;

    public float speedMultiplier;
    
    public List<GameObject> winds = new List<GameObject>();
    public List<float> baseWindsEmi = new List<float>();
    
    private void Start()
    {
        power = 0;
        actualTime = time;
        foreach (var wind in winds)
        {
            baseWindsEmi.Add(wind.GetComponent<ParticleSystem>().emission.rateOverTimeMultiplier);
        }
        
    }

    private void Update()
    {
        actualTime -= Time.deltaTime;

        if (actualTime <= 0)
        {
            ascending = !ascending;
            actualTime = time;
        }

        if (@ascending)
        {
            power = Mathf.MoveTowards(power, maxPower, ascendingTime);
        }
        else
        {
            power = Mathf.MoveTowards(power, 0f, ascendingTime);
        }


        for (int i = 0; i < winds.Count; i++)
        {
            var vel = winds[i].GetComponent<ParticleSystem>().velocityOverLifetime;
            var emi = winds[i].GetComponent<ParticleSystem>().emission;
            
            vel.speedModifier = new ParticleSystem.MinMaxCurve(power * speedMultiplier);
        
            
            if(power <= 0)
                winds[i].SetActive(false);
            else
            {
                winds[i].SetActive(true);
                emi.rateOverTime = new ParticleSystem.MinMaxCurve(baseWindsEmi[i] * (power + 1));
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            other.GetComponent<FireWind>().velocity = direction * power;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            other.GetComponent<FireWind>().velocity = Vector3.zero;
        }
    }
}

