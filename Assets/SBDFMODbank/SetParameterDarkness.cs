using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class SetParameterDarkness : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

  
    [Range(0f, 3f)]
    public float darkness;
    
  


    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        instance.setParameterByName("darkness", darkness);
      
        
    }

    public void MusicStop()
    {
        instance.stop(STOP_MODE.IMMEDIATE);
    }
}
