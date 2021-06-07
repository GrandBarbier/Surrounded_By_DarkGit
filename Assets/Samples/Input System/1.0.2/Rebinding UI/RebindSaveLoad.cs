using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;

public class RebindSaveLoad : MonoBehaviour
{
   public InputActionAsset actions;
   
   public RebindActionUI[] RebindActionUis;

   /*void OnEnable()
   {
      var rebinds = PlayerPrefs.GetString("rebinds");
      if (!string.IsNullOrEmpty(rebinds))
      {
         actions.LoadFromJson(rebinds);
      }
   }
   
   void OnDisable()
   {
      //var rebinds = actions.Save
   }*/

   void Awake()
   {
      InputAction[] actionsArrray = actions.ToArray();
      
      for (int i = 0; i < actions.Count(); i++)
      {
         if (!string.IsNullOrEmpty(PlayerPrefs.GetString(actionsArrray[i].name)))
         {
            //get saved binding index
            int bindingIndex = PlayerPrefs.GetInt(actionsArrray[i].name + "bindingIndex");
            
            //create new input with saved input
            InputBinding t = actionsArrray[i].bindings[bindingIndex];
            t.overridePath = PlayerPrefs.GetString(actionsArrray[i].name + " key");
            
            //apply new input
            actionsArrray[i].ChangeBinding(bindingIndex).To(t);
            Debug.Log($"set saved input : {actionsArrray[i].name} -> {actionsArrray[i].bindings[bindingIndex].effectivePath}");
         }
      }

      for (int i = 0; i < RebindActionUis.Count(); i++)
      {
         RebindActionUis[i].UpdateBindingDisplay();
      }
   }
}
