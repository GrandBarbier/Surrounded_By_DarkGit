using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpTorch : MonoBehaviour
{
    public GameObject player;
    public GameObject player_hand;
    public GameObject torchHandPos;
    public GameObject torch;
    public Animator animator;
    
    private Action<InputAction.CallbackContext> pickUpTorchAction;

    void Start()
    {
        pickUpTorchAction = context => TriggerPickUpTorch();
        Gears.gears.playerInput.actions["PoseTorch"].performed += pickUpTorchAction;
    }

    void OnDestroy()
    {
        if (Gears.gears.playerInput != null)
        {
            Gears.gears.playerInput.actions["PoseTorch"].performed -= pickUpTorchAction;
        }
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("quelqu'un stay ici");
        // if (Input.GetKeyDown(KeyCode.F) && player.GetComponent<PlaceTorch>().torchOnGround && torch.GetComponent<WaterTorch>().canBePicked)
        // {
        //     player.GetComponent<Movement>().animPlaying = true;
        //     animator.SetTrigger("PickTorch");
        //     Debug.Log("pickup");
        //     player.GetComponent<PlaceTorch>().torchOnGround = false;
        //     
        // }
    }



    void PickTorch()
    {
        StartCoroutine(CoolDown());
        torch.transform.parent = player_hand.transform;
        torch.transform.position = torchHandPos.transform.position;
        torch.transform.rotation = torchHandPos.transform.rotation;
        torch.transform.localEulerAngles += new Vector3(0, -90, 0);
    }

    public void TriggerPickUpTorch()
    {
        if (player.GetComponent<PlaceTorch>().torchOnGround && !animator.GetCurrentAnimatorStateInfo(0).IsName("Torch Drop") && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Empty Pick Up") && player.GetComponent<Movement>().isGrounded && torch.GetComponent<WaterTorch>().canBePicked)
        {
            player.GetComponent<Movement>().animPlaying = true;
            //animator.SetTrigger("PickTorch");
            animator.Play("Empty Pick Up");
            Debug.Log("pickup");
        }
    }

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.65f);
        torch.GetComponent<WaterTorch>().canBePicked = false;
        player.GetComponent<PlaceTorch>().torchOnGround = false;
    }
}
