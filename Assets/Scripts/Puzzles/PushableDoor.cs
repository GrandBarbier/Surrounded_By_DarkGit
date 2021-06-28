using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class PushableDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject player;


    public float limit;
    public float speed;

    public bool pushable;

    [Header("References")] 
    public Movement playerMovement;
    public PlaceTorch placeTorch;
    
    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;
    public bool audioPlayed;
    
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance,  GetComponent<Transform>(), GetComponent<Rigidbody>());

        audioPlayed = false;
        pushable = true;
    }
    
    void Update()
    {
        if (door.transform.rotation.z < limit)
        {
            pushable = false;
            instance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
    
    PLAYBACK_STATE PlaybackState(EventInstance instance)
    {
        PLAYBACK_STATE pS;
        instance.getPlaybackState(out pS);
        return pS;        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Gears.gears.playerInput.actions["Interact"].ReadValue<float>() > 0 && playerMovement.isGrounded && placeTorch.torchOnGround && pushable)
            {
                if (!audioPlayed)
                {
                    audioPlayed = true;
                    instance.start();
                }
                door.transform.Rotate(0,0,speed);
            }
            else
            {
                instance.stop(STOP_MODE.IMMEDIATE);
                audioPlayed = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        instance.stop(STOP_MODE.IMMEDIATE);
        audioPlayed = false;
    }

    private void OnValidate()
    {
        GetReferenceComponents();
    }

    private void Reset()
    {
        GetReferenceComponents();
    }

    public void GetReferenceComponents()
    {
        playerMovement = player.GetComponent<Movement>();
        placeTorch = player.GetComponent<PlaceTorch>();
    }
}
