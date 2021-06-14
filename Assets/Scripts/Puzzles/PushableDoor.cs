using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject player;
    
    public float speed;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.A) && player.GetComponent<Movement>().isGrounded && player.GetComponent<PlaceTorch>().torchOnGround)
            {
               door.transform.Rotate(0,0,speed);
            }
        }
    }
}
