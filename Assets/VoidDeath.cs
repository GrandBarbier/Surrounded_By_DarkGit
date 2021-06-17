using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDeath : MonoBehaviour
{
    public GameObject gameManager;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.GetComponent<GameplayManager>().Death();
    }
}
