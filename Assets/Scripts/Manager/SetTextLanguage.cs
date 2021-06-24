using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTextLanguage : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI text;
    
    void Start()
    {
        text = MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(gameObject, useParent: true)[0];
        SetText();
        LanguageSystem.setLanguageEvent += SetText;
    }
    
    void Update()
    {
        
    }

    public void SetText()
    {
        if (text != null && LanguageSystem.outputData.Length > id)
        {
             text.text = LanguageSystem.outputData[id];
        }
      
    }
}
