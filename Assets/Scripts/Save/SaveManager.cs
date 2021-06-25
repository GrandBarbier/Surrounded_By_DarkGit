using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player.LoadPlayer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            player.SavePlayer();
        }
    }
}
