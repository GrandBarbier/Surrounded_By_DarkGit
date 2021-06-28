using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class pressingPlate : MonoBehaviour
{
    public GameObject door;
    public Transform direction;
    public GameObject link;
    public Transform borrow;
    public Renderer ren;
    public pressingPlate plate;

    public Material[] mats;
    public Material good;

    public bool isOpened = false;

    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;

    public bool audioPlayed;
    
     void Start()
     {
         audioPlayed = false;
         mats = ren.materials;
         GetReferenceComponents();
     }

    void Update()
    {
        if (isOpened && plate.isOpened == true)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, direction.position, Time.deltaTime);
            
        }

        if (isOpened)
        {
            transform.position = Vector3.MoveTowards(transform.position, borrow.position, Time.deltaTime);

            mats[3] = good;
            ren.materials = mats;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PushableBlock")
        {
            isOpened = true;
            if (!audioPlayed)
            {
                instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance,  GetComponent<Transform>(), GetComponent<Rigidbody>());
                instance.start();
                audioPlayed = true;
            }
        }
    }
    
    // private void Reset()
    // {
    //     GetReferenceComponents();
    // }

    public void GetReferenceComponents()
    {
        plate = link.GetComponent<pressingPlate>();
        ren = gameObject.GetComponent<Renderer>();
    }
}
