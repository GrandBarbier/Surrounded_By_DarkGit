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

    [Header("References")] 
    public Movement playerMovement;
    public PlaceTorch placeTorch;
    
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
            if (Gears.gears.playerInput.actions["Interact"].ReadValue<float>() > 0 && playerMovement.isGrounded && placeTorch.torchOnGround && pushable)
            {
               door.transform.Rotate(0,0,speed);
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
    }
}
