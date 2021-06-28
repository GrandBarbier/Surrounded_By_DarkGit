using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class igniteBrazero : MonoBehaviour
{
	public GameObject brazero_light;
	public GameObject brazero_particles;
	public GameObject torch_light;
	public GameObject torch_particles;
	public bool brazeroOn;
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{

			if (torch_light.activeSelf)
			{
				if (!brazero_light.activeSelf)
				{
					//lancer animation
					brazeroOn = true;
				}
			}
			else
			{
				if (brazero_light.activeSelf)
				{
					//lancer animation

					torch_light.SetActive(true);
					torch_particles.SetActive(true);
				}
			}
		}
	}

	private void Update()
	{
		if (brazeroOn)
		{
			brazero_light.SetActive(true);
			brazero_particles.SetActive(true);
		}
		else
		{
			brazero_light.SetActive(false);
			brazero_particles.SetActive(false);
		}
	}
}
