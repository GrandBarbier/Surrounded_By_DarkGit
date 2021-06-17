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
    
    void Start()
    {
        placeTorchAction = context => TriggerDropTorchAnim();
        Gears.gears.playerInput.actions["PoseTorch"].performed += placeTorchAction;
    }
    
    void OnDestroy()
    {
        if (Gears.gears.playerInput != null)
        {
            Gears.gears.playerInput.actions["PoseTorch"].performed -= placeTorchAction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var dropTorchEvent = new AnimationEvent();
        dropTorchEvent.functionName = "DropTorch";
//         if (Input.GetKeyDown((KeyCode.E)) && torchOnGround == false && player.GetComponent<Movement>().isGrounded)
//         {
//             player.GetComponent<Movement>().animPlaying = true;
//             animator.SetTrigger("DropTorch");
// //            Debug.Log("place");
//             
//             
//         }
    }

    void DropTorch()
    {
//        Debug.Log("poser");
        //rend la torche indépendante en mettant à jour la variable l'indiquant
        StartCoroutine(CoolDown());
        torch.transform.parent = null;
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
    
    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.65f);
        torchOnGround = true;
        torch.GetComponent<WaterTorch>().canBePicked = true;
    }
}
