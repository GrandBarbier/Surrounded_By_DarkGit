using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    
    [Header("Interactive Objects")]
    public List<GameObject> interactiveObjects = new List<GameObject>();
    
    [Header("Braseros")]
    public List<igniteBrazero> braseros = new List<igniteBrazero>();

    [Header("Torch")] 
    public GameObject torch;
    public PlaceTorch placeTorch;
    
    [Header("Leviers")]
    public List<GameObject> leviers = new List<GameObject>();
    public List<LevierManche> levierManche = new List<LevierManche>();
    public List<firststepbutton> firststepbuttons = new List<firststepbutton>();

    public static bool load;
    
    private void Start()
    {
        if (load)
        {
            LoadGame();
            load = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            load = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();
        
        Vector3 playerPosition;
        playerPosition.x = data.playerPosition[0];
        playerPosition.y = data.playerPosition[1];
        playerPosition.z = data.playerPosition[2];
        player.transform.position = playerPosition;

        Quaternion playerRotation;
        playerRotation.x = data.playerRotation[0];
        playerRotation.y = data.playerRotation[1];
        playerRotation.z = data.playerRotation[2];
        playerRotation.w = data.playerRotation[3];
        player.transform.rotation = playerRotation;

        for (int i = 0; i < data.objectId.Length; i++)
        {
            Vector3 objectPosition;
            objectPosition.x = data.objectPosX[i];
            objectPosition.y = data.objectPosY[i];
            objectPosition.z = data.objectPosZ[i];
            interactiveObjects[i].transform.position = objectPosition;

            Quaternion objectRotation;
            objectRotation.x = data.objectRotX[i];
            objectRotation.y = data.objectRotY[i];
            objectRotation.z = data.objectRotZ[i];
            objectRotation.w = data.objectRotW[i];
            interactiveObjects[i].transform.rotation = objectRotation;
        }

        for (int i = 0; i < data.levierId.Length; i++)
        {
            Vector3 levierPosition;
            levierPosition.x = data.levierPosX[i];
            levierPosition.y = data.levierPosY[i];
            levierPosition.z = data.levierPosZ[i];
            leviers[i].transform.position = levierPosition;

            levierManche[i].activated = data.levierActivated[i];
            firststepbuttons[i].neverused = data.levierNeverused[i];
        }

        Vector3 torchPos;
        torchPos.x = data.torchPos[0];
        torchPos.y = data.torchPos[1];
        torchPos.z = data.torchPos[2];
        torch.transform.position = torchPos;

        Quaternion torchRot;
        torchRot.x = data.torchRot[0];
        torchRot.y = data.torchRot[1];
        torchRot.z = data.torchRot[2];
        torchRot.w = data.torchRot[3];
        torch.transform.rotation = torchRot;

        placeTorch.torchOnGround = data.torchOnGround;
    }
}
