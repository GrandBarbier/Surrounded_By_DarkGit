using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWind : MonoBehaviour
{
    private  ParticleSystem.VelocityOverLifetimeModule vel;
    private  ParticleSystem.VelocityOverLifetimeModule vel_1;

    public float forceToExtinguish;
    public GameObject light;

    public Vector3 velocity;

    public float fireMultiplier = 1;
    public float embersMultiplier = 1;
    
    void Start()
    {
        vel = gameObject.GetComponent<ParticleSystem>().velocityOverLifetime;
        vel_1 = gameObject.GetComponentsInChildren<ParticleSystem>()[1].velocityOverLifetime;

        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.World;
        
        vel_1.enabled = true;
        vel_1.space = ParticleSystemSimulationSpace.World;
    }

    // Update is called once per frame
    void Update()
    {
        vel.x = new ParticleSystem.MinMaxCurve(velocity.x * fireMultiplier);
        vel.y = new ParticleSystem.MinMaxCurve(velocity.y * fireMultiplier);
        vel.z = new ParticleSystem.MinMaxCurve(velocity.z * fireMultiplier);
        
        vel_1.x = new ParticleSystem.MinMaxCurve(velocity.x * embersMultiplier);
        vel_1.y = new ParticleSystem.MinMaxCurve(velocity.y * embersMultiplier);
        vel_1.z = new ParticleSystem.MinMaxCurve(velocity.z * embersMultiplier);
        

        if (velocity.x < forceToExtinguish || velocity.y < forceToExtinguish || velocity.z < forceToExtinguish)
        {
            gameObject.SetActive(false);
            light.SetActive(false);
        }
    }
}
