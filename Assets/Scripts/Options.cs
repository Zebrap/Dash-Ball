using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;
    private float musicValue;
    private float efxValue;
    public Slider musicVolueSlider;
    public Slider efxVolueSlider;
    public Dropdown dropDownQuality;

    private void Start()
    {
        LoadOptionsToSliders();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetEfxVolume(float volume)
    {
        audioMixer.SetFloat("Efx", volume);
    }


    public void LoadOptionsToSliders()
    {
        /*
        musicValue = PlayerPrefs.GetFloat("Music", 0);
        efxValue = PlayerPrefs.GetFloat("Efx", 0);

        audioMixer.SetFloat("Music", musicValue);
        audioMixer.SetFloat("Efx", efxValue);*/

        if (audioMixer.GetFloat("Music", out float musicValue))
        {
            musicVolueSlider.value = musicValue;
        }

        if (audioMixer.GetFloat("Efx", out float efxValue))
        {
            efxVolueSlider.value = efxValue;
        }
        dropDownQuality.value = QualitySettings.GetQualityLevel()-1; // not very low (-1)
    }

    public void SetQuality(int qualityIndex)
    {
        // low, medium, high (not very low)
        QualitySettings.SetQualityLevel(qualityIndex+1);
    }

    public void SaveOptions()
    {
        if(audioMixer.GetFloat("Music", out float musicValue))
        {
            PlayerPrefs.SetFloat("Music", musicValue);
        }

        if (audioMixer.GetFloat("Efx", out float efxValue))
        {
            PlayerPrefs.SetFloat("Efx", efxValue);
        }
        // Quality:
        PlayerPrefs.SetInt("QualityLevel", QualitySettings.GetQualityLevel());

        PlayerPrefs.Save();
    }
}
