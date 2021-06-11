using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushBox : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    private void Start()
    {
        player = this.gameObject;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PushableBlock"))
        {
            if (Input.GetKey(KeyCode.A) && player.GetComponent<Movement>().isGrounded &&
                player.GetComponent<PlaceTorch>().torchOnGround)
            {
                //setbool
            }
   
        }
    }
}
