using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InGameToolTip : MonoBehaviour
{
    public InputActionReference action;

    public TextMeshProUGUI TextMeshPro;

    public bool playerClose;

    private bool _done;

    [Tooltip("it will replace {key} by the current input")]
    public bool adaptInput;
    
    private List<string> devices = new List<string>();
    private List<string> keys = new List<string>();

    void Start()
    {
        action.action.performed += context => TryDone();

        UpdateToolTipText();
    }
    
    void Update()
    {
        
    }

    public void TryDone()
    {
        if (playerClose)
        {
            //Debug.LogWarning("tipsDone");
            _done = true;
        }
    }

    public void UpdateToolTipText()
    {
         for (int i = 0; i < action.action.bindings.Count; i++)
        {
            string device = null;
            string key = null;
            bool getDevice = true;
            
            for (int j = 1; j < action.action.bindings[i].path.ToCharArray().Length; j++)
            {
                if (!getDevice)
                {
                    key += action.action.bindings[i].path.ToCharArray()[j];
                }
                
                if (action.action.bindings[i].path.ToCharArray()[j].ToString() == ">")
                {
                    getDevice = false;
                    j++;
                }else if (getDevice)
                {
                    device += action.action.bindings[i].path.ToCharArray()[j];
                }
            }
            
            devices.Add(device);
            keys.Add(key);
        }

        for (int i = 0; i < devices.Count; i++)
        {
            if (Gamepad.current != null && devices[i] == "Gamepad")
            {
                Debug.LogWarning(devices[i] + " " + keys[i]);
            }else if (devices[i] == "Keyboard")
            {
                //Debug.LogWarning(new StringBuilder(gameObject.name + " " + action.name + " " + devices[i] + " " + keys[i], 50));

                if (adaptInput)
                {
                    for (int j = 0; j < TextMeshPro.text.ToCharArray().Length; j++)
                    {
                        //Find "key" in the text and replace it
                        if (TextMeshPro.text.ToCharArray()[j].ToString() == "k" && TextMeshPro.text.ToCharArray()[j + 1].ToString() == "e" &&
                            TextMeshPro.text.ToCharArray()[j + 2].ToString() == "y")
                        {
                            List<Char> chars = TextMeshPro.text.ToCharArray().ToList();

                            chars.RemoveAt(j);
                            chars.RemoveAt(j);
                            chars.RemoveAt(j);

                            for (int k = 0; k < keys[i].ToCharArray().Length; k++)
                            {
                                chars.Insert(j, keys[i].ToCharArray()[k]);
                            }
                            
                            StringBuilder sb = new StringBuilder(50);
                            sb.Append(chars.ToArray());
                            TextMeshPro.text = sb.ToString();
                            //Debug.LogWarning(sb.ToString());
                            break;
                        }
                    }
                }
            }
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
