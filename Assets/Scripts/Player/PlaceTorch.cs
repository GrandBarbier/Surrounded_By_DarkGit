using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceTorch : MonoBehaviour
{
    public bool torchOnGround;
    public GameObject torch;
    public GameObject torchOnGroundPos;
    public GameObject player;
    public Animator animator;
    // Start is called before the first frame update
    
    private Action<InputAction.CallbackContext> placeTorchAction;
    public float poseTorchThreshHold;
    private float _poseTorchValue;
    
    void Start()
    {
        placeTorchAction = context => _poseTorchValue = 0;
        //placeTorchAction = context => TriggerDropTorchAnim();
        //Gears.gears.playerInput.actions["PoseTorch"].performed += placeTorchAction;
        Gears.gears.playerInput.actions["DropTorch"].canceled += placeTorchAction;
    }
    
    void OnDestroy()
    {
        if (Gears.gears.playerInput != null)
        {
            //Gears.gears.playerInput.actions["PoseTorch"].performed -= placeTorchAction;
            Gears.gears.playerInput.actions["DropTorch"].canceled -= placeTorchAction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //var dropTorchEvent = new AnimationEvent();
        //dropTorchEvent.functionName = "DropTorch";
//         if (Input.GetKeyDown((KeyCode.E)) && torchOnGround == false && player.GetComponent<Movement>().isGrounded)
//         {
//             player.GetComponent<Movement>().animPlaying = true;
//             animator.SetTrigger("DropTorch");
// //            Debug.Log("place");
//             
//             
//         }

        if (Gears.gears.playerInput.actions["DropTorch"].ReadValue<float>() > 0)
        {
            _poseTorchValue += Time.deltaTime;

            if (_poseTorchValue >= poseTorchThreshHold)
            {
                TriggerDropTorchAnim();
            }
        }
    }

    void DropTorch()
    {
//        Debug.Log("poser");
        //rend la torche indépendante en mettant à jour la variable l'indiquant
        
        torchOnGround = true;
        torch.GetComponent<WaterTorch>().canBePicked = true;
        
        torch.transform.parent = null;

        torch.GetComponent<Rigidbody>().isKinematic = false;
        torch.GetComponent<Rigidbody>().useGravity = true;
        torch.GetComponent<MeshCollider>().isTrigger = false;
    }

    public void TriggerDropTorchAnim()
    {
        if (torchOnGround == false && player.GetComponent<Movement>().isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("Empty Pick Up") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Torch Drop"))
        {
            player.GetComponent<Movement>().animPlaying = true;
            //animator.SetTrigger("DropTorch");
            animator.Play("Torch Drop");
            //Debug.Log("place");
        }
    }
}
