using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameToolTip : MonoBehaviour
{
    public InputActionReference action;

    public TextMeshProUGUI TextMeshPro;

    public bool playerClose;

    void Start()
    {
        action.action.performed += context => TryDestruction(gameObject);
    }
    
    void Update()
    {
        
    }

    public void TryDestruction(GameObject go)
    {
        Debug.LogWarning("TryDestruct");
        
        if (playerClose)
        {
            Destroy(go);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerClose = true;
            TextMeshPro?.gameObject.SetActive(true);
        }
    }
    
    public void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerClose = false;
            TextMeshPro?.gameObject.SetActive(false);
        }
    }
}
