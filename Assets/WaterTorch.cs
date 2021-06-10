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
        Debug.Log(other.gameObject.tag);
        canBePicked = true;
        if (other.gameObject.CompareTag("water"))
        {
            t_light.SetActive(false);
            particles.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canBePicked = false;
    }
}
