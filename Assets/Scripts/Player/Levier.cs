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
            if (Input.GetKey(KeyCode.A) && player.GetComponent<Movement>().isGrounded &&
                player.GetComponent<PlaceTorch>().torchOnGround)
            {
                
                playerAnimator.SetBool("IsHanging", true);
                player.GetComponent<Movement>().animPlaying = true;

            }

            if (hanged)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, place.position, Time.deltaTime * 4);
                player.transform.rotation = place.transform.rotation;
                hanged = false;
            }

            if (player.transform.position == place.position)
            {
                manche.transform.position = Vector3.MoveTowards(manche.transform.position, direction.position, Time.deltaTime);
            }
            
            
            if(manche.position == direction.position)
            {
                playerAnimator.SetBool("IsHanging", false);
                Debug.Log("good");
                player.GetComponent<Movement>().animPlaying = false;
                manche.GetComponent<LevierManche>().activated = true;
                manche.GetComponent<firststepbutton>().neverused = true;
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
}
