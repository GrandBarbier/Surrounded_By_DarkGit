using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableBlock : MonoBehaviour
{
    public Vector3 forceOfPush;
    public float speed;
    public GameObject block;
    public GameObject player;

    [Header("References")] 
    public Movement playerMovement;
    public PlaceTorch placeTorch;
    public Rigidbody blockRb;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.A) && playerMovement.isGrounded && placeTorch.torchOnGround)
            {
                blockRb.AddRelativeForce(forceOfPush * (Time.deltaTime * speed));

                //block.transform.position += forceOfPush * (Time.deltaTime * speed);
                /*lancer anim pose des mains
                while (Input.GetKey(KeyCode.A))
                {
                    boucler anim effort
                    block.transform.position += forceOfPush * (Time.deltaTime * speed);
                }
                lancer anim enlevage des mains
                */
            }
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
        playerMovement = player.GetComponent<Movement>();
        placeTorch = player.GetComponent<PlaceTorch>();
        blockRb = block.GetComponent<Rigidbody>();
    }
}
