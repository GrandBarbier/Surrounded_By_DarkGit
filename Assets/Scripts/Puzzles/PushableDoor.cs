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
    
    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;
    
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
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
                instance.start();
               door.transform.Rotate(0,0,speed);
            }
            else
            {
                instance.stop();
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
