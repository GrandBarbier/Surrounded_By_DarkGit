using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightSensor : MonoBehaviour
{
    public GameplayManager gameplayManager;
    
    public RenderTexture lightCheckTexture;
    public float lightLevel;
    public float lightLimit;

    public float deathLevel;
    public float deathTime;

    [Header("Game Objects")] 
    public GameObject panel;
    
    private void Start()
    {
      

        deathLevel = 0;


    }

    void Update()
    {
        LightCheck();
        
        DeathLevel();
    }

    public void LightCheck()
    {
        RenderTexture tmpTexture = RenderTexture.GetTemporary(lightCheckTexture.width, lightCheckTexture.height, 0,
            RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(lightCheckTexture, tmpTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tmpTexture;

        Texture2D temp2DTexture = new Texture2D(lightCheckTexture.width, lightCheckTexture.height);
        temp2DTexture.ReadPixels(new Rect(0, 0, tmpTexture.width, tmpTexture.height), 0, 0);
        temp2DTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tmpTexture);

        Color[] colors = temp2DTexture.GetPixels(1);

        lightLevel = 0;
        for (int i = 0; i < colors.Length; i++)
        {
            //lightLevel += (0.02126f * colors[i].r) + (0.07152f * colors[i].g) + (0.00722f * colors[i].b);
            lightLevel += (colors[i].r + colors[i].g + colors[i].b)/1500;
        }

        //Debug.Log(lightLevel);
    }

    public void DeathLevel()
    {
       
        if (deathLevel >= deathTime)
        {
            gameplayManager.Death();
            deathLevel = deathTime;
        }

        if (lightLevel <= lightLimit)
        {
            if (lightLevel > 0)
            {
                deathLevel += Time.deltaTime *(1 - lightLevel)/2;
            }
            else
            {
                deathLevel += Time.deltaTime;
            }
            
        }
        else
        {
            deathLevel -= Time.deltaTime * lightLevel/5;
        }
        
        if (deathLevel <= 0)
        {
            deathLevel = 0;
        }
        
       panel.GetComponent<Image>().material.SetFloat("_VignetteScale", deathLevel/deathTime * 5);
    }
}
