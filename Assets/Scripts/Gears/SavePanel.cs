using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SavePanel : MonoBehaviour
{
    public TextMeshProUGUI pathText;
    public int pathTextId;
    public int saveTextId;
    public string path;

    public GameObject browseButton;
    
    PlayerInput _controls;
    public static ControlDeviceType currentControlDevice;

    public Transform saveDisplayParent;
    public int menuIndex;
    public int columnIndex;
    public int saveCount;
    private Menu saveMenu;
    
    public enum ControlDeviceType{
        KeyboardAndMouse,
        Gamepad,
    }
    
    void Start()
    {
        UpdatePathText(Application.persistentDataPath);
        LanguageSystem.setLanguageEvent += () => UpdatePathText(path);
        LanguageSystem.setLanguageEvent += UpdateSaveNames;

        if (Gamepad.current != null)
        {
            browseButton.SetActive(false);
        }
        else
        {
            //Debug.Log("No GamePad");
        }

        saveMenu = Gears.gears.menuManager.menus.Find(menu1 => menu1.index == menuIndex);

        saveCount = saveMenu.menuMap[columnIndex].list.Capacity;

        //_controls = Gears.gears.playerInput;
        //_controls.onControlsChanged += OnControlsChanged;
    }
    
    void Update()
    {
        
    }

    void OnEnable() {
        InputUser.onChange += onInputDeviceChange;
        InputUser.onChange += (user, change, arg3) => Debug.Log(arg3);
    }
 
    void OnDisable() {
        InputUser.onChange -= onInputDeviceChange;
    }

    public void UpdateSaveNames()
    {
        for (int i = 0; i < saveMenu.menuMap[columnIndex].list.Count; i++)
        {
            TextMeshProUGUI t = MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(saveMenu.menuMap[columnIndex].list[i].gameObject, useParent: true)[0];
            
            if (i.ToString().ToCharArray().Length == 1)
            {
                t.text = $"{LanguageSystem.outputData[saveTextId]}_0{i}";
            }
            else
            {
                t.text += $"{LanguageSystem.outputData[saveTextId]}_{i}";
            }
        }
    }

    public void AddSaveDisplay()
    {
        RectTransform previousDisplayPos = null;
        
        if (saveMenu.menuMap[columnIndex].list.Capacity > 0)
        {
            previousDisplayPos = saveMenu.menuMap[columnIndex].list[saveMenu.menuMap[columnIndex].list.Count - 1];
        }
        
        GameObject saveDisplay = Instantiate(Gears.gears.saveDisplayPrefab, saveDisplayParent);
        saveMenu.menuMap[columnIndex].list.Add(saveDisplay.GetComponent<RectTransform>());
        
        //Save Name
        saveCount++;
        string saveName = $"{LanguageSystem.outputData[saveTextId]}_";
        
        //Debug.Log(saveCount.ToString().ToCharArray().Length);
        if (saveCount.ToString().ToCharArray().Length == 1)
        {
            saveName += "0" + saveCount;
        }
        else
        {
            saveName += saveCount;
        }
        
        saveDisplay.GetComponentInChildren<TextMeshProUGUI>().text = saveName;

        if (previousDisplayPos != null)
        {
            saveDisplay.GetComponent<RectTransform>().localPosition = previousDisplayPos.localPosition + new Vector3(0, -30, 0);
        }

        Gears.gears.menuManager.SetCurrentMenuMap(Gears.gears.menuManager.ConvertListToPanelMap(saveMenu.menuMap, saveMenu.startPos), 
            new Vector2Int(Gears.gears.menuManager.selection.posOnMap.x, Gears.gears.menuManager.selection.posOnMap.y));
    }

    public void UpdatePathText(string s)
    {
        path = s;
        pathText.text = $"{LanguageSystem.outputData[pathTextId]} : {s}";
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
