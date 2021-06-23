using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBGMintro : MonoBehaviour

    
{

    FMOD.Studio.EventInstance Sound;
    // Start is called before the first frame update
    void Start()
    {
        Sound = FMODUnity.RuntimeManager.CreateInstance("event:/BGM_Intro");
    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Sound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    private void OnTriggerEnter(Collider other)
    {
        Sound.start();
    }

    private void OnTriggerExit(Collider other)
    {
        Sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
