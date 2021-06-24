using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerValueFMOD : MonoBehaviour
{
	public bool musicChanging;
	public bool end;
	public bool needsToGoUp;
	public int trackId;
	private IEnumerator _musicChange;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(MusicChange());
		}
	}

	public IEnumerator MusicChange() {
		float delay = 0.03f;
		musicChanging = true;
		Debug.Log("on est la");
		if (needsToGoUp)
		{
			if (trackId == 1)
			{
				while (GetComponentInParent<SetParameterByName>().introWater < 1) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().introWater += 0.01f;
				}
			}
			else if (trackId == 2)
			{
				while (GetComponentInParent<SetParameterByName>().waterMaze < 1) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().waterMaze += 0.01f;
				}
			}
			else if (trackId == 3)
			{
				while (GetComponentInParent<SetParameterByName>().mazeWind < 1) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().mazeWind += 0.01f;
				}
			}
		}
		else
		{
			if (trackId == 1)
			{
				while (GetComponentInParent<SetParameterByName>().introWater > 0) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().introWater -= 0.01f;
				}
			}
			else if (trackId == 2)
			{
				while (GetComponentInParent<SetParameterByName>().waterMaze > 0) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().waterMaze -= 0.01f;
				}
			}
			else if (trackId == 3)
			{
				while (GetComponentInParent<SetParameterByName>().mazeWind > 0) {
					yield return new WaitForSeconds(delay);
					GetComponentInParent<SetParameterByName>().mazeWind -= 0.01f;
				}
			}
		}
		musicChanging = false;
	}
}
