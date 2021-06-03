using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class SavePanel : MonoBehaviour
{
    public TextMeshProUGUI pathText;
    public string path;

    public RectTransform scrollable;
    public float sensibility = 1f;
    private Vector2 startPos;
    public Vector2 maxPos;
    private float progress;

    public GameObject browseButton;
    
    PlayerInput _controls;
    public static ControlDeviceType currentControlDevice;
    public enum ControlDeviceType{
        KeyboardAndMouse,
        Gamepad,
    }
    
    void Start()
    {
        UpdatePathText(Application.persistentDataPath);

        startPos = scrollable.position;
        maxPos = startPos + maxPos;

        if (Gamepad.current != null)
        {
            browseButton.SetActive(false);
        }
        else
        {
            Debug.Log("No GamePad");
        }
        
        //_controls = Gears.gears.playerInput;
        //_controls.onControlsChanged += OnControlsChanged;
    }
    
    void Update()
    {
        progress += -Mouse.current.scroll.ReadValue().y * 0.001f * sensibility;

        progress = Mathf.Clamp(progress, 0f, 1f);

        scrollable.position = Vector2.Lerp(startPos, maxPos, progress);
    }

    void OnEnable() {
        InputUser.onChange += onInputDeviceChange;
    }
 
    void OnDisable() {
        InputUser.onChange -= onInputDeviceChange;
    }

    public void UpdatePathText(string s)
    {
        path = s;
        pathText.text = $"path : {s}";
    }
    
    //Solution 1
    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged && user.controlScheme != null) 
        {
            switch (user.controlScheme.Value.name)
            {
                case "Gamepad" :
                    Debug.Log("last device used : Gamepad");
                    break;
                case "Keyboard" :
                    Debug.Log("last device used : Keyboard");
                    break;
            }
        }
    }
    
    //Solution 2
    private void OnControlsChanged(PlayerInput obj)
    {
        if (obj.currentControlScheme == "Gamepad")
        {
            if (currentControlDevice != ControlDeviceType.Gamepad)
            {
                currentControlDevice = ControlDeviceType.Gamepad;
                // Send Event
                // EventHandler.ExecuteEvent("DeviceChanged", currentControlDevice);
            }
        }
        else
        {
            if (currentControlDevice != ControlDeviceType.KeyboardAndMouse)
            {
                currentControlDevice = ControlDeviceType.KeyboardAndMouse;
                // Send Event
                // EventHandler.ExecuteEvent("DeviceChanged", currentControlDevice);
            }
        }
        
        Debug.Log(currentControlDevice);
    }
}
