using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushBox : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Animator playerAnimator;

    private void Start()
    {
        player = this.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("tag touché par trigger player : " + other.gameObject.tag);
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
        else
        {
            playerAnimator.SetBool("IsPushing", false);
        }
        
    }   
}
