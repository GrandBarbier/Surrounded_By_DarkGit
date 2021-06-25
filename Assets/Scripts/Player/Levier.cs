using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : MonoBehaviour
{
    [Header("Animation")]
    public GameObject player;
    public Animator playerAnimator;
    public Transform direction;
    public Transform manche;
    public Transform place;
    public bool hanged;

    [Header("References")] 
    public Movement playerMovement;
    public PlaceTorch placeTorch;
    public LevierManche levierManche;
    public firststepbutton firstStepButton;

    private void Start()
    {
        player = this.gameObject;
    }
    
    

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("levier"))
        {
            direction = other.gameObject.transform.GetChild(2);
            manche = other.gameObject.transform.GetChild(1);
            place = manche.gameObject.transform.GetChild(0);
            if (Input.GetKey(KeyCode.A) && playerMovement.isGrounded &&
                placeTorch.torchOnGround)
            {
                
                playerAnimator.SetBool("IsHanging", true);
                playerMovement.animPlaying = true;

            }

            if (hanged)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, place.position, Time.deltaTime * 4);
                player.transform.rotation = place.transform.rotation;
            }

            if (player.transform.position == place.position)
            {
                manche.transform.position = Vector3.MoveTowards(manche.transform.position, direction.position, Time.deltaTime);
            }
            
            
            if(manche.position == direction.position)
            {
                playerAnimator.SetBool("IsHanging", false);
                Debug.Log("good");
                playerMovement.animPlaying = false;
                levierManche.activated = true;
                firstStepButton.neverused = true;
            }
        } 
    }

    public void JumpHang()
    {
        hanged = true;
    }

    private void OnTriggerExit(Collider other)
    {
        direction = null;
        manche = null;
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
        levierManche = manche.GetComponent<LevierManche>();
        firstStepButton = manche.GetComponent<firststepbutton>();
        
        playerMovement = player.GetComponent<Movement>();
        placeTorch = player.GetComponent<PlaceTorch>();
    }
}
