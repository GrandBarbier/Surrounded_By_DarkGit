using System.Collections;
using System.Collections.Generic;
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
        }
    }
}
