using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string InputFootsteps;
    FMOD.Studio.EventInstance FootstepsEvent;
    //FMOD.Studio.ParameterInstance WoodParameter;
    //FMOD.Studio.ParameterInstance MetalParameter;
    //FMOD.Studio.ParameterInstance GrassParameter;

    bool playerismoving;
    public float walkingspeed;
     private float WoodValue;
    private float MetalValue;
    private float GrassValue;
    private bool playerisgrounded;

    void Start()
    {
        FootstepsEvent = FMODUnity.RuntimeManager.CreateInstance(InputFootsteps);
        //FootstepsEvent.getParameter("Wood", out WoodParameter);
        //FootstepsEvent.getParameter("Metal", out MetalParameter);
        //FootstepsEvent.getParameter("Grass", out GrassParameter);

        InvokeRepeating("CallFootsteps", 0, walkingspeed);
    }

    void Update()
    {
        //WoodParameter.setValue(WoodValue);
        //MetalParameter.setValue(MetalValue);
        //GrassParameter.setValue(GrassValue);

        if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            if (playerisgrounded == true)
            {
                playerismoving = true;
            }
            else if (playerisgrounded == false)
            {
                playerismoving = false;
            }
        }
        else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            playerismoving = false;
        }
    }

    void CallFootsteps()
    {
        if (playerismoving == true)
        {
            FootstepsEvent.start();
        }
        else if (playerismoving == false)
        {
            //Debug.Log ("player is moving = false");
        }
    }

    void OnDisable()
    {
        playerismoving = false;
    }

    void OnTriggerStay(Collider MaterialCheck)
    {
        //float FadeSpeed = 10f;
        playerisgrounded = true;

        if (MaterialCheck.CompareTag("Wood:Material"))
        {
            WoodValue = 1f;
            MetalValue = 0f;
            GrassValue = 0f;
        }
        if (MaterialCheck.CompareTag("Metal:Material"))
        {
            WoodValue = 0f;
            MetalValue = 1f;
            GrassValue = 0f;
        }
        if (MaterialCheck.CompareTag("Grass:Material"))
        {
            WoodValue = 0f;
            MetalValue = 0f;
            GrassValue = 1f;
        }
    }

    void OnTriggerExit(Collider MaterialCheck)
    {
        playerisgrounded = false;
    }
}