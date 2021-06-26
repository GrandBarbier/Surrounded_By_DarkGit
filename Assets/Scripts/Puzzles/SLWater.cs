using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLWater : MonoBehaviour
{
    public Transform dir1;
    public Transform dir2;

    public bool goingDir1;

    public float speed;
    public float waitTime;
    
    void Update()
    {
        if (goingDir1)
        {
            transform.position = Vector3.MoveTowards(transform.position, dir1.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, dir2.position, speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == dir1.gameObject || other.gameObject == dir2.gameObject)
        {
            StartCoroutine("Wait", waitTime);
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        goingDir1 = !goingDir1;
    }
}
