using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class CinemachineController : MonoBehaviour
{
    public float lookSpeedX;
    public float lookSpeedY;
    
    public float lookSpeedXController;
    public float lookSpeedYController;
    public CinemachineFreeLook cinemachineFreeLook;

    void Start()
    {
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }
    
    void Update()
    {
        Vector2 delta = Gears.gears.playerInput.actions["CameraMove"].ReadValue<Vector2>();
        
        if (Gamepad.current != null)
        {
            cinemachineFreeLook.m_XAxis.Value += delta.x * lookSpeedXController * Time.deltaTime;
            cinemachineFreeLook.m_YAxis.Value -= delta.y * lookSpeedYController * Time.deltaTime;
        }
        else
        {
            cinemachineFreeLook.m_XAxis.Value += delta.x * lookSpeedX * Time.deltaTime;
            cinemachineFreeLook.m_YAxis.Value -= delta.y * lookSpeedY * Time.deltaTime;
        }
    }
    
    void OnEnable()
    {
        Gears.gears.playerInput.onControlsChanged += OnControlsChanged;
        //InputUser.onChange += (user, change, arg3) => Debug.Log(arg3);
    }
    
    private void OnControlsChanged(PlayerInput obj)
    {
        if (obj.currentControlScheme == "Gamepad")
        {
            if (SavePanel.currentControlDevice != SavePanel.ControlDeviceType.Gamepad)
            {
                SavePanel.currentControlDevice = SavePanel.ControlDeviceType.Gamepad;
                // Send Event
                // EventHandler.ExecuteEvent("DeviceChanged", currentControlDevice);
            }
        }
        else
        {
            if (SavePanel.currentControlDevice != SavePanel.ControlDeviceType.KeyboardAndMouse)
            {
                SavePanel.currentControlDevice = SavePanel.ControlDeviceType.KeyboardAndMouse;
                // Send Event
                // EventHandler.ExecuteEvent("DeviceChanged", currentControlDevice);
            }
        }
        
        Debug.Log(SavePanel.currentControlDevice);
    }
}
