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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("puuuuuuuute");
            if (Input.GetKey(KeyCode.A) && player.GetComponent<Movement>().isGrounded && player.GetComponent<PlaceTorch>().torchOnGround)
            {
                block.transform.position += forceOfPush * (Time.deltaTime * speed);
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
}
