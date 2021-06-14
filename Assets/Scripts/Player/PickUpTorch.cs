using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTorch : MonoBehaviour
{
    public GameObject player;
    public GameObject player_hand;
    public GameObject torchHandPos;
    public GameObject torch;
    public Animator animator;

    
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("quelqu'un stay ici");
        if (Input.GetKeyDown(KeyCode.F) && player.GetComponent<PlaceTorch>().torchOnGround && torch.GetComponent<WaterTorch>().canBePicked)
        {
            player.GetComponent<Movement>().animPlaying = true;
            animator.SetTrigger("PickTorch");
            Debug.Log("pickup");
            player.GetComponent<PlaceTorch>().torchOnGround = false;
            
        }
    }



    void PickTorch()
    {
        torch.transform.parent = player_hand.transform;
        torch.transform.position = torchHandPos.transform.position;
        torch.transform.rotation = torchHandPos.transform.rotation;
        torch.transform.localEulerAngles += new Vector3(0, -90, 0);
        
    }
}
