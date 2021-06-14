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

    public bool isOpened = false;

    void Update()
    {
        if (isOpened && link.GetComponent<pressingPlate>().isOpened == true)
        {
            Debug.Log("aled");

            door.transform.position = Vector3.MoveTowards(door.transform.position, direction.position, Time.deltaTime);
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
