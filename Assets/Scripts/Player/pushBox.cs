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

    private void OnTriggerStay(Collision other)
    {
        if (other.gameObject.CompareTag("PushableBlock"))
        {
            Debug.Log("tag detected");
            if (Input.GetKey(KeyCode.A) && player.GetComponent<Movement>().isGrounded &&
                player.GetComponent<PlaceTorch>().torchOnGround)
            {
                playerAnimator.SetBool("IsPushing", true);
                Debug.Log("launch anim");
            }     
        }
        else 
                playerAnimator.SetBool("IsPushing", false);
    }   
}
