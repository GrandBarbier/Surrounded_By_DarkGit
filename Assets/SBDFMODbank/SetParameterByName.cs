using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParameterByName : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

  
    [Range(0f, 1f)]
    public float introWater;
    
    [Range(0f, 1f)]
    public float waterMaze;
    
    [Range(0f, 1f)]
    public float mazeWind;
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        instance.setParameterByName("introWater", introWater);
        instance.setParameterByName("waterMaze", waterMaze);
        instance.setParameterByName("mazeWind", mazeWind);
    }

    void MusicStop()
    {
       instance.stop();
    }
}
