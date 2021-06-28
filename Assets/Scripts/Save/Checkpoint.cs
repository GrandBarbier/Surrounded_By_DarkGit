using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject brazero_light;
    public GameObject brazero_particles;
    public GameObject brazero_SFX;
    public GameObject torch_light;
    public GameObject torch_particles;
    public GameObject torch_SFX;
    public bool brazeroOn;
    public bool brazeroOnLastFrame;
    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;

    public SaveManager saveManager;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (torch_light.activeSelf)
            {
                if (!brazero_light.activeSelf)
                {
                    brazeroOn = true;
                }
            }
            else
            {
                if (brazero_light.activeSelf)
                {
                    Debug.Log("nique ta mere");
                    torch_light.SetActive(true);
                    torch_particles.SetActive(true);
                    torch_SFX.SetActive(true);
                    //jouer ignition
                    Debug.Log("nique ta mere1");

                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance,  GetComponent<Transform>(), GetComponent<Rigidbody>());
                    instance.start();
                    torch_light.transform.parent.gameObject.GetComponent<WaterTorch>().torchOff = false;
                    Debug.Log("nique ta mere2");

                }
            }
        }
    }

    private void Update()
    {
        if (!brazeroOnLastFrame)
        {
            if (brazeroOn)
            {
                brazero_light.SetActive(true);
                brazero_particles.SetActive(true);
                brazero_SFX.SetActive(true);
                //jouer ignition
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance,  GetComponent<Transform>(), GetComponent<Rigidbody>());
                instance.start();
                saveManager.SaveGame();
            }
            else
            {
                brazero_light.SetActive(false);
                brazero_particles.SetActive(false);
                brazero_SFX.SetActive(false);
            }
        }
        brazeroOnLastFrame = brazeroOn;

    }
}
