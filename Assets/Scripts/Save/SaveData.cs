using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float[] playerPosition;
    public float[] playerRotation;

    public float[] objectId;
    public float[] objectPosX;
    public float[] objectPosY;
    public float[] objectPosZ;
    public float[] objectRotX;
    public float[] objectRotY;
    public float[] objectRotZ;
    public float[] objectRotW;


    public float[] levierId;
    public float[] levierPosX;
    public float[] levierPosY;
    public float[] levierPosZ;
    public bool[] levierActivated;
    public bool[] levierNeverused;
    
    
    
    public SaveData(SaveManager save)
    {
        playerPosition = new float[3];
        playerPosition[0] = save.player.transform.position.x;
        playerPosition[1] = save.player.transform.position.y;
        playerPosition[2] = save.player.transform.position.z;
        
        playerRotation = new float[4];
        playerRotation[0] = save.player.transform.rotation.x;
        playerRotation[1] = save.player.transform.rotation.y;
        playerRotation[2] = save.player.transform.rotation.z;
        playerRotation[3] = save.player.transform.rotation.w;
        
        
        objectId = new float[save.interactiveObjects.Count];
        objectPosX = new float[objectId.Length];
        objectPosY = new float[objectId.Length];
        objectPosZ = new float[objectId.Length];
        
        objectRotX = new float[objectId.Length];
        objectRotY = new float[objectId.Length];
        objectRotZ = new float[objectId.Length];
        objectRotW = new float[objectId.Length];

        for (int i = 0; i < objectId.Length; i++)
        {
            objectPosX[i] = save.interactiveObjects[i].transform.position.x;
            objectPosY[i] = save.interactiveObjects[i].transform.position.y;
            objectPosZ[i] = save.interactiveObjects[i].transform.position.z;

            objectRotX[i] = save.interactiveObjects[i].transform.rotation.x;
            objectRotY[i] = save.interactiveObjects[i].transform.rotation.y;
            objectRotZ[i] = save.interactiveObjects[i].transform.rotation.z;
            objectRotW[i] = save.interactiveObjects[i].transform.rotation.w;
        }
        
        levierId = new float[save.leviers.Count];
        levierPosX = new float[levierId.Length];
        levierPosY = new float[levierId.Length];
        levierPosZ = new float[levierId.Length];
        
        levierActivated = new bool[levierId.Length];
        levierNeverused = new bool[levierId.Length];
        
        for (int i = 0; i < levierId.Length; i++)
        {
            levierPosX[i] = save.leviers[i].transform.position.x;
            levierPosY[i] = save.leviers[i].transform.position.y;
            levierPosZ[i] = save.leviers[i].transform.position.z;
            
            levierActivated[i] = save.levierManche[i].activated;
            levierNeverused[i] = save.firststepbuttons[i].neverused;
        }
    }
}
