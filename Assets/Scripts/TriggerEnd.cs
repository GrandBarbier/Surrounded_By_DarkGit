using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerEnd : MonoBehaviour
{
    public float endFadeTime;
    public Image panel;
    public Movement playerMovements;
    public SetParameterByName setParameterByName;
    public SetParameterDarkness setParameterDarkness;
    public Animator animator;

    public bool end;

    public float alpha;


    private void Start()
    {
        panel.color = new Color(255,255,255,0);
        end = false;
    }

    private void Update()
    {
        alpha = panel.color.a;
        
        if (panel.color.a == 1)
        {
            setParameterByName.instance.stop(STOP_MODE.ALLOWFADEOUT);
            setParameterDarkness.MusicStop();
            SceneManager.LoadScene(0);
        }

        if (end)
        {
            playerMovements.enabled = false;
            animator.Play("Empty Idle 1");

            var tempColor = panel.color;
            
            tempColor.a = Mathf.MoveTowards(tempColor.a, 1, endFadeTime * Time.deltaTime);
            panel.color = tempColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            end = true;
            
        }
    }
}
