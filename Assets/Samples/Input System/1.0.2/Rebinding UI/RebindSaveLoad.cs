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
      InputAction[] actionsArray = actions.ToArray();

      for (int i = 0; i < actions.Count(); i++)
      {
         int bindingIndex = PlayerPrefs.GetInt(actionsArray[i].name + "bindingIndex");

         //Debug.Log(actionsArray[i].name + " : " + actionsArray[i].bindings.Count);
         
         RebindSavedBind(actionsArray[i], bindingIndex);
      }

      //Update display
      for (int i = 0; i < RebindActionUis.Count(); i++)
      {
         RebindActionUis[i].UpdateBindingDisplay();
      }
   }

   public void RebindSavedBind(InputAction action, int bindingIndex)
   {
      if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.name)))
      {
         //Debug.Log(action.name + " index : " + bindingIndex + " Count : " + action.bindings.Count);
         
         if (action.bindings[bindingIndex].isComposite || action.bindings[bindingIndex].isPartOfComposite)
         {
            var nextBindingIndex = bindingIndex + 1;
            if (nextBindingIndex < action.bindings.Count && action.bindings[nextBindingIndex].isPartOfComposite)
            {
               //Debug.Log("rebind next composite key : " + nextBindingIndex + " binding count : " + action.bindings.Count);
               Rebind(action, nextBindingIndex, true);
               RebindSavedBind(action, nextBindingIndex);
            }
         }
         else
         {
            Rebind(action, bindingIndex);
         }
      }
   }

   public void Rebind(InputAction action, int bindingIndex, bool allCompositePart = false)
   {
      //Debug.Log("Rebind Key : " + action.name + " / " + action.bindings[bindingIndex].effectivePath);

      //create new input with saved input
      //Debug.Log("Action : " + action.name + " count : " + action.bindings.Count + " index : " + bindingIndex);
      InputBinding t = action.bindings[bindingIndex];
      
      if (allCompositePart)
      {
         t.overridePath = PlayerPrefs.GetString(action.name + " key" + bindingIndex);
      }
      else
      {
         t.overridePath = PlayerPrefs.GetString(action.name + " key");
      }
      
      //apply new input
      action.ChangeBinding(bindingIndex).To(t);
      Debug.Log($"set saved input : {action} -> {action.bindings[bindingIndex].effectivePath}");
   }
}
