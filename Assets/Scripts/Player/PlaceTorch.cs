using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Text;

public class PlaceTorch : MonoBehaviour
{
    public bool torchOnGround;
    public GameObject torch;
    public GameObject torchSFX;
    public GameObject torchSFX2;
    public GameObject torchOnGroundPos;
    public GameObject player;
    public Animator animator;
    // Start is called before the first frame update
    
    private Action<InputAction.CallbackContext> placeTorchAction;
    public float poseTorchThreshHold;
    private float _poseTorchValue;

    [Header("Torch Component")]
    public Rigidbody rb;
    public WaterTorch waterTorch;
    public MeshCollider torchMesh;
    
    void Start()
    {
        placeTorchAction = context => _poseTorchValue = 0;
        //placeTorchAction = context => TriggerDropTorchAnim();
        //Gears.gears.playerInput.actions["PoseTorch"].performed += placeTorchAction;
        Gears.gears.playerInput.actions["DropTorch"].canceled += placeTorchAction;

        if (!torchOnGround)
        {
            animator.Play("Empty Pick Up");
        }
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


        if (torchOnGround)
        {
            torch.transform.parent = null;

            rb.isKinematic = false;
            rb.useGravity = true;
            torchMesh.isTrigger = false;
            torchSFX.SetActive(false);
            torchSFX2.SetActive(true);
        }
    }

    void DropTorch()
    {
//        Debug.Log("poser");
        //rend la torche indépendante en mettant à jour la variable l'indiquant
        
        torchOnGround = true;
    }

    public void TriggerDropTorchAnim()
    {
        player.TryGetComponent(out Movement movement);
        if (torchOnGround == false && movement.isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("Empty Pick Up") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Torch Drop"))
        {
            movement.animPlaying = true;
            //animator.SetTrigger("DropTorch");
            animator.Play("Torch Drop");
            //Debug.Log("place");
        }
    }

    private void OnValidate()
    {
        GetTorchComponent();
    }

    private void Reset()
    {
        GetTorchComponent();
    }

    private void GetTorchComponent()
    {
        if (torch != null)
        {
            rb = torch.GetComponent<Rigidbody>();
            waterTorch = torch.GetComponent<WaterTorch>();
            torchMesh = torch.GetComponent<MeshCollider>();
        }
    }
}
