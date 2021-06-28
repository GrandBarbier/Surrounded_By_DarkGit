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

    [Header("Components")] 
    public WaterTorch waterTorch;
    public Rigidbody torchRb;
    public MeshCollider torchMeshCollider;
    public PlaceTorch placeTorch;
    public Movement playerMovement;
    
    
    private Action<InputAction.CallbackContext> pickUpTorchAction;

    void Start()
    {
        pickUpTorchAction = context => TriggerPickUpTorch();
        Gears.gears.playerInput.actions["DropTorch"].performed += pickUpTorchAction;
    }

    void OnDestroy()
    {
        if (Gears.gears.playerInput != null)
        {
            Gears.gears.playerInput.actions["DropTorch"].performed -= pickUpTorchAction;
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

    void Update()
    {
        if (!placeTorch.torchOnGround)
        {
            waterTorch.canBePicked = false;
            
            torchRb.useGravity = false;
            torchRb.isKinematic = true;
            torchMeshCollider.isTrigger = true;
        
            torch.transform.parent = player_hand.transform;
            torch.transform.position = torchHandPos.transform.position;
            torch.transform.rotation = torchHandPos.transform.rotation;
            torch.transform.localEulerAngles += new Vector3(0, -90, 0);
        }
    }

    void PickTorch()
    {
        placeTorch.torchOnGround = false;
    }

    public void TriggerPickUpTorch()
    {
        if (placeTorch.torchOnGround && !animator.GetCurrentAnimatorStateInfo(0).IsName("Torch Drop") && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Empty Pick Up") && playerMovement.isGrounded && waterTorch.canBePicked)
        {
            playerMovement.animPlaying = true;
            //animator.SetTrigger("PickTorch");
            animator.Play("Empty Pick Up");
            Debug.Log("pickup");
        }
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
        waterTorch = torch.GetComponent<WaterTorch>();
        torchRb = torch.GetComponent<Rigidbody>();
        torchMeshCollider = torch.GetComponent<MeshCollider>();
        playerMovement = player.GetComponent<Movement>();
        placeTorch = player.GetComponent<PlaceTorch>();
    }
}
