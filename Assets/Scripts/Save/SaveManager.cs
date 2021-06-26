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
    
    
    private void Start()
    {
        //LoadGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            LoadGame();
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
    }
}
