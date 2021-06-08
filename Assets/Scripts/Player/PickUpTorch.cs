using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTorch : MonoBehaviour
{
    public bool canBePicked;
    public GameObject player;
    public GameObject torchHandPos;
    public GameObject t_light;
    public GameObject particles;
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        canBePicked = true;
        if (other.gameObject.CompareTag("water"))
        {
            t_light.SetActive(false);
            particles.SetActive(false);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("quelqu'un stay ici");
        if (Input.GetKeyDown(KeyCode.F) && player.GetComponent<PlaceTorch>().torchOnGround && canBePicked)
        {
            Debug.Log("pickup");
            gameObject.transform.parent = player.transform;
            gameObject.transform.position = torchHandPos.transform.position;
            gameObject.transform.rotation = torchHandPos.transform.rotation;
            gameObject.transform.localEulerAngles += new Vector3(0, -90, 0);
            player.GetComponent<PlaceTorch>().torchOnGround = false;
            
            animator.SetBool("HasTorch", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canBePicked = false;
    }
}
