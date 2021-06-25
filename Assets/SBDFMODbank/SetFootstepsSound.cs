using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFootstepsSound : MonoBehaviour
{
	public Movement movement;
	
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
		instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		instance.start();
	}

	// Update is called once per frame
	void Update()
	{
		if (isInWater)
		{
			water = 1;
			stone = 0;
		}
		else if (movement.isGrounded)
		{
			stone = 1;
			water = 0;
		}
		else
		{
			stone = 0;
			water = 0;
		}

		instance.setParameterByName("Stone", stone);
		instance.setParameterByName("Water", water);
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
	
	private void OnValidate()
	{
		GetReferenceComponents();
	}

	private void Reset()
	{
		GetReferenceComponents();
	}

	public void GetReferenceComponents()
	{
		movement = GetComponent<Movement>();
	}
}