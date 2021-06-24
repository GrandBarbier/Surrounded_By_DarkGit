using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameToolTip : MonoBehaviour
{
    public InputActionReference action;

    public TextMeshProUGUI TextMeshPro;

    public bool playerClose;

    public bool _done;

    void Start()
    {
        action.action.performed += context => TryDone();
    }
    
    void Update()
    {
        
    }

    public void TryDone()
    {
        if (playerClose)
        {
            Debug.LogWarning("tipsDone");
            _done = true;
        }
    }
    
    public static IEnumerator FadeDuration(object obj, Color start, Color end, float duration, bool setActiveFalse = true, Action onComplete = null)
    {
        switch (obj)
        {
            case TextMeshProUGUI text :
                text.gameObject.SetActive(true);
                text.color = start;
                
                for (float t = 0f; t < duration; t += Time.deltaTime) 
                {
                    float normalizedTime = t/duration;

                    if (text)
                    {
                        text.color = Color.Lerp(start, end, normalizedTime);
                    }
                    else
                    {
                        break;
                    }
         
                    yield return null;
                }
                
                if (text)
                {
                    text.color = end;

                    if (setActiveFalse)
                    {
                        text.gameObject.SetActive(false);
                    }
                }
                break;
        }

        onComplete?.Invoke();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerClose = true;
            
            StopAllCoroutines();
            
            StartCoroutine(FadeDuration(TextMeshPro, 
                new Color(TextMeshPro.color.r, TextMeshPro.color.g, TextMeshPro.color.b, 0), 
                new Color(TextMeshPro.color.r, TextMeshPro.color.g, TextMeshPro.color.b, 1), 0.5f, false));
        }
    }
    
    public void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerClose = false;

            StopAllCoroutines();
            
            if (_done)
            {
                StartCoroutine(FadeDuration(TextMeshPro, TextMeshPro.color, 
                    new Color(TextMeshPro.color.r, TextMeshPro.color.g, TextMeshPro.color.b, 0), 0.5f, false, () => Destroy(gameObject)));
            }
            else
            {
                StartCoroutine(FadeDuration(TextMeshPro, TextMeshPro.color, 
                    new Color(TextMeshPro.color.r, TextMeshPro.color.g, TextMeshPro.color.b, 0), 0.5f, false));
            }
        }
    }
}
