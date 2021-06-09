using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetAllBindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActionAsset;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void ResetBindings()
    {
        for (int i = 0; i < _inputActionAsset.actionMaps.Count; i++)
        {
            _inputActionAsset.actionMaps[i].RemoveAllBindingOverrides();

            for (int j = 0; j < _inputActionAsset.actionMaps[i].actions.Count; j++)
            {
                PlayerPrefs.DeleteKey(_inputActionAsset.actionMaps[i].actions[j].name);
                PlayerPrefs.DeleteKey(_inputActionAsset.actionMaps[i].actions[j].name + " key");
                PlayerPrefs.DeleteKey(_inputActionAsset.actionMaps[i].actions[j].name + "bindingIndex");
            }
        }
    }
}
