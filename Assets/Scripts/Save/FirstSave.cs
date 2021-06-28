using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSave : MonoBehaviour
{
    public static bool saved;

    public SaveManager saveManager;
    
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (!saved && other.CompareTag("Player"))
        {
            saveManager.SaveGame();
            saved = true;
        }
    }
}
