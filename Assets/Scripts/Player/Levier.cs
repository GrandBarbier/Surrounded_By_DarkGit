using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;

public class Levier : MonoBehaviour
{
    [Header("Animation")]
    public GameObject player;
    public Animator playerAnimator;
    public Transform direction;
    public Transform manche;
    public Transform place;
    public bool hanged;

    private GameObject currentLever;
    private List<GameObject> leverDone = new List<GameObject>();
    private bool playerClose;
    public Action<InputAction.CallbackContext> tryTriggerAnim;
    public bool audioPlaying;
    
    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;

    private void Start()
    {
        player = this.gameObject;
        
        tryTriggerAnim = context => TryTriggerAnimation();
        Gears.gears.playerInput.actions["Interact"].performed += tryTriggerAnim;
        audioPlaying = false;
    }
    
    

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("levier"))
        {
            direction = other.gameObject.transform.GetChild(2);
            manche = other.gameObject.transform.GetChild(1);
            place = manche.gameObject.transform.GetChild(0);
            // if (Input.GetKeyDown(KeyCode.A) && player.GetComponent<Movement>().isGrounded &&
            //     player.GetComponent<PlaceTorch>().torchOnGround)
            // {
            //     
            //     playerAnimator.SetBool("IsHanging", true);
            //     player.GetComponent<Movement>().animPlaying = true;
            //
            // }

            if (hanged)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, place.position, Time.deltaTime * 4);
                player.transform.rotation = place.transform.rotation;
            }

            if (player.transform.position == place.position)
            {
                manche.transform.position = Vector3.MoveTowards(manche.transform.position, direction.position, Time.deltaTime);

                if (!audioPlaying)
                {
                    instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, currentLever.GetComponent<Transform>(), currentLever.GetComponent<Rigidbody>());
                    instance.start();
                    audioPlaying = true;
                }
            }
            
            
            if(manche.position == direction.position)
            {
                playerAnimator.SetBool("IsHanging", false);
                Debug.Log("good");
                player.GetComponent<Movement>().animPlaying = false;
                manche.GetComponent<LevierManche>().activated = true;
                manche.GetComponent<firststepbutton>().neverused = true;
                hanged = false;
                leverDone.Add(currentLever);
                
                instance.stop(STOP_MODE.IMMEDIATE);
                audioPlaying = false;
            }
        } 
    }

    public void TryTriggerAnimation()
    {
        if (playerClose && player.GetComponent<Movement>().isGrounded && player.GetComponent<PlaceTorch>().torchOnGround && !leverDone.Contains(currentLever))
        {
            //FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, currentLever.transform,  currentLever.GetComponent<Rigidbody>());
            instance.start();
            
            playerAnimator.SetBool("IsHanging", true);
            player.GetComponent<Movement>().animPlaying = true;
        }
    }

    public void JumpHang()
    {
        hanged = true;
    }

    void OnDestoy()
    {
        Gears.gears.playerInput.actions["Interact"].performed -= tryTriggerAnim;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("levier"))
        {
            playerClose = true;
            currentLever = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        direction = null;
        manche = null;
        
        if (other.gameObject.CompareTag("levier"))
        {
            playerClose = false;
        }
    }
}