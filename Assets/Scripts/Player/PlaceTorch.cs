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
        if (Input.GetKeyDown((KeyCode.E)) && torchOnGround == false && player.GetComponent<Movement>().isGrounded)
        {
            animator.SetTrigger("DropTorch");
            animator.SetBool("HasTorch", false);
            Debug.Log("place");
            
            
        }
    }

    void DropTorch()
    {
        
        //rend la torche indépendante en mettant à jour la variable l'indiquant
        torch.transform.parent = null;
        torchOnGround = true;
    }
}
