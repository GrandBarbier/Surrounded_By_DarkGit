using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject player;


    public float limit;
    public float speed;

    public bool pushable;
    
    void Start()
    {
        pushable = true;
    }
    
    void Update()
    {
        if (door.transform.rotation.z < limit)
        {
            pushable = false;
        }
//        Debug.Log(door.transform.rotation.z);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.A) && player.GetComponent<Movement>().isGrounded && player.GetComponent<PlaceTorch>().torchOnGround && pushable)
            {
               door.transform.Rotate(0,0,speed);
            }
        }
    }
}
