using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveVolume : MonoBehaviour
{
    public Slider sliderGeneralVolume;
    public Slider sliderEffectVolume;
    public Slider sliderMusicVolume;
    
    void Start()
    {
        sliderGeneralVolume.value = PlayerPrefs.GetFloat(sliderGeneralVolume.name, sliderGeneralVolume.value);
        sliderEffectVolume.value = PlayerPrefs.GetFloat(sliderEffectVolume.name, sliderEffectVolume.value);
        sliderMusicVolume.value = PlayerPrefs.GetFloat(sliderMusicVolume.name, sliderMusicVolume.value);
    }
    
    void Update()
    {
        
    }

    public void SaveSliderValue(Slider slider)
    {
        Debug.Log(slider.name);
        PlayerPrefs.SetFloat(slider.name, slider.value);
    }
}
