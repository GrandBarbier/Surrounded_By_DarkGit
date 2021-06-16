using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
	public AnimationCurve lightCurve;
	public float fireSpeed = 1f;

	private Light _lightComp;
	private float _initialIntensity;

	private Vector3 _pos;

	public float magnitude = 1f;
	public float magnitudeIntensity = 1f;

	private void Awake()
	{
		_lightComp = GetComponent<Light>();
		_initialIntensity = _lightComp.intensity;
		_pos = transform.localPosition;
	}

	void Update()
	{
		_lightComp.intensity = _initialIntensity * lightCurve.Evaluate(Time.time * fireSpeed) * magnitudeIntensity;

		transform.localPosition = transform.up * (Mathf.Sin(lightCurve.Evaluate(Time.time * fireSpeed)) * magnitude);

		Debug.Log(lightCurve.Evaluate(Time.time));

	}
}
