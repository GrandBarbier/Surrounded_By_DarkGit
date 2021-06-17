using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTorch : MonoBehaviour
{
    public bool canBePicked;
    public GameObject t_light;
    public GameObject particles;
    private void OnTriggerEnter(Collider other)
    {
//        Debug.Log(other.gameObject.tag);
        if (other.tag == "Player")
        {
            canBePicked = true;
        }
        
        if (other.gameObject.CompareTag("water"))
        {
            t_light.SetActive(false);
            particles.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canBePicked = false;
        }
    }
}
