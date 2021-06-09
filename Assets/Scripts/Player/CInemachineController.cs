using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CInemachineController : MonoBehaviour
{
    public float lookSpeedX;
    public float lookSpeedY;
    public CinemachineFreeLook cinemachineFreeLook;
    
    void Start()
    {
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }
    
    void Update()
    {
        Vector2 delta = Gears.gears.playerInput.actions["CameraMove"].ReadValue<Vector2>();

        cinemachineFreeLook.m_XAxis.Value += delta.x * lookSpeedX * Time.deltaTime;
        cinemachineFreeLook.m_YAxis.Value -= delta.y * lookSpeedY * Time.deltaTime;
    }
}
