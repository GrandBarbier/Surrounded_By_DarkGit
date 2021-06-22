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

    public Material[] mats;
    public Material good;

    public bool isOpened = false;

     void Start()
     {
         ren = GetComponent<Renderer>();
         mats = ren.materials;
     }

    void Update()
    {
        if (isOpened && link.GetComponent<pressingPlate>().isOpened == true)
        {
            Debug.Log("aled");

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
        }
        
        
        
    }
}
