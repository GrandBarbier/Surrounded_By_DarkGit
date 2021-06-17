using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : MonoBehaviour
{
    public GameObject player;
    public Animator playerAnimator;
    public Transform direction;
    public GameObject manche;

    private void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("levier"))
        {
            direction = other.gameObject.transform.GetChild(0);
            if (Input.GetKey(KeyCode.A) && player.GetComponent<Movement>().isGrounded &&
                player.GetComponent<PlaceTorch>().torchOnGround)
            {
                Debug.Log("launch anim");
                playerAnimator.SetBool("IsHanging", true);
                other.transform.position = Vector3.MoveTowards(other.transform.position, direction.position, Time.deltaTime);
                Debug.Log("launched anim");
                
            }
            else
            {
                playerAnimator.SetBool("IsHanging", false);
            }
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        direction = null;
    }
}
