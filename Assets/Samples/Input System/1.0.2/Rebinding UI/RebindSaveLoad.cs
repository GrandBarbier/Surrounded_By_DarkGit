using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
   public InputActionAsset actions;

   void OnEnable()
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
   }
}
