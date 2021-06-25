using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class SetFootstepsSound : MonoBehaviour
{
	private FMOD.Studio.EventInstance instance;
	
	[FMODUnity.EventRef]
	public string fmodEvent;

  
	[Range(0f, 1f)]
	public float stone;
    
	[Range(0f, 1f)]
	public float water;
	public bool isInWater;
    
	
	// Start is called before the first frame update
	void Start()
	{
		InitSound();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void InitSound()
	{
		instance = RuntimeManager.CreateInstance(fmodEvent);
		instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
		RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
	}
	
	void Footsteps()
	{
		if (isInWater)
		{
			water = 1;
			stone = 0;
			instance.setParameterByName("Stone", stone);
			instance.setParameterByName("Water", water);
			instance.start();

		}
		else if (gameObject.GetComponent<Movement>().isGrounded)
		{
			stone = 1;
			water = 0;
			instance.setParameterByName("Stone", stone);
			instance.setParameterByName("Water", water);
			instance.start();
		}
		else
		{
			stone = 0;
			water = 0;
			instance.setParameterByName("Stone", stone);
			instance.setParameterByName("Water", water);
			instance.start();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.CompareTag("water"))
		{
			isInWater = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.CompareTag("water"))
		{
			isInWater = false;
		}
	}
}