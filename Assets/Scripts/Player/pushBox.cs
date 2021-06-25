using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushBox : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Animator playerAnimator;

    [Header("References")] 
    public Movement playerMovement;
    public PlaceTorch placeTorch;

    private void Start()
    {
        player = this.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("tag touché par trigger player : " + other.gameObject.tag);
        if (other.gameObject.CompareTag("sideBox"))
        {
            Debug.Log("tag detected");
            if (Input.GetKey(KeyCode.A) && player.GetComponent<Movement>().isGrounded &&
                player.GetComponent<PlaceTorch>().torchOnGround)
            {
                Debug.Log("launch anim");
                playerAnimator.SetBool("IsPushing", true);
                Debug.Log("launched anim");
                
            }
            else
            {
                playerAnimator.SetBool("IsPushing", false);
            }
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        playerAnimator.SetBool("IsPushing", false);
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
