using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class pushableBlock : MonoBehaviour
{
    public Vector3 forceOfPush;
    public float speed;
    public GameObject block;
    public GameObject player;

    [Header("References")] 
    public Movement playerMovement;
    public PlaceTorch placeTorch;
    public Rigidbody blockRb;

    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;
    public bool audioPlayed;
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Gears.gears.playerInput.actions["Interact"].ReadValue<float>() > 0 && playerMovement.isGrounded && placeTorch.torchOnGround)
            {
                if (!audioPlayed)
                {
                    audioPlayed = true;
                    instance.start();
                }
                blockRb.AddRelativeForce(forceOfPush * (Time.deltaTime * speed));
            }
            else
            {
                instance.stop(STOP_MODE.ALLOWFADEOUT);
                audioPlayed = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        instance.stop(STOP_MODE.ALLOWFADEOUT);
        audioPlayed = false;
    }

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance,  GetComponent<Transform>(), GetComponent<Rigidbody>());

        audioPlayed = false;
        GetReferenceComponents();
    }

    // private void Reset()
    // {
    //     GetReferenceComponents();
    // }

    public void GetReferenceComponents()
    {
        playerMovement = player.GetComponent<Movement>();
        placeTorch = player.GetComponent<PlaceTorch>();
        blockRb = block.GetComponent<Rigidbody>();
    }
}
