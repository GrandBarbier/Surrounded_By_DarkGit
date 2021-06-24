using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerValueFMOD : MonoBehaviour
{
	public bool musicChanging;
	public bool needsToGoUp;
	public int trackId;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			MusicChange();
	}
	
	public IEnumerator MusicChange() {
		float delay = 0.2f;
		musicChanging = true;
		if (needsToGoUp)
		{
			if (trackId == 1)
			{
				while (GetComponentInParent<SetParameterByName>().introWater < 1) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().introWater += 0.1f;
				}
			}
			else if (trackId == 2)
			{
				while (GetComponentInParent<SetParameterByName>().waterMaze < 1) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().waterMaze += 0.1f;
				}
			}
			else if (trackId == 3)
			{
				while (GetComponentInParent<SetParameterByName>().mazeWind < 1) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().mazeWind += 0.1f;
				}
			}
		}
		else
		{
			if (trackId == 1)
			{
				while (GetComponentInParent<SetParameterByName>().introWater > 0) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().introWater -= 0.1f;
				}
			}
			else if (trackId == 2)
			{
				while (GetComponentInParent<SetParameterByName>().waterMaze > 0) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().waterMaze -= 0.1f;
				}
			}
			else if (trackId == 3)
			{
				while (GetComponentInParent<SetParameterByName>().mazeWind > 0) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().mazeWind -= 0.1f;
				}
			}
		}
		musicChanging = false;
	}
}
