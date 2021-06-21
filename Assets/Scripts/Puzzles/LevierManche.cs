using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierManche : MonoBehaviour
{
    // Start is called before the first frame update
    public bool activated = false;

    public GameObject door;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, target.position, Time.deltaTime);
        }
    }
}
