using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTorch : MonoBehaviour
{
    public bool torchOnGround;
    public GameObject torch;
    public GameObject torchOnGroundPos;
    public GameObject player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dropTorchEvent = new AnimationEvent();
        dropTorchEvent.functionName = "DropTorch";
        if (Input.GetKeyDown((KeyCode.E)) && torchOnGround == false && player.GetComponent<Movement>().isGrounded)
        {
            player.GetComponent<Movement>().animPlaying = true;
            animator.SetTrigger("DropTorch");
//            Debug.Log("place");
            
            
        }
    }

    void DropTorch()
    {
//        Debug.Log("poser");
        //rend la torche indépendante en mettant à jour la variable l'indiquant
        torch.transform.parent = null;
        torchOnGround = true;
    }
}
