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

    public float speedOffDivider;

    private Image panelImage;

    [Header("Game Objects")] 
    public GameObject panel;

    public SetParameterDarkness setParameterDarkness;

    private void Start()
    {

        panelImage = panel.GetComponent<Image>();
        deathLevel = 0;


    }

    void Update()
    {
        LightCheck();
        
        DeathLevel();

        setParameterDarkness.darkness = deathLevel;
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
        Destroy(temp2DTexture);

        lightLevel = 0;
        for (int i = 0; i < colors.Length; i++)
        {
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
            deathLevel -= Time.deltaTime * lightLevel/speedOffDivider;
        }
        
        if (deathLevel <= 0)
        {
            deathLevel = 0;
        }
        
        panelImage.material.SetFloat("_VignetteScale", deathLevel/deathTime * 5);
    }
}
