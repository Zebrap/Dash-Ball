using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Audio players components.
    public AudioSource efxSource;
    public AudioSource musicSource;

    // Random pitch adjustment range.
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    // Singleton instance.
    public static SoundManager instance = null;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadOptions();
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }


    public void SetEfxVolume(float volume)
    {
        efxSource.volume = volume;
    }

    public void LoadOptions()
    {
        // sound
        if (PlayerPrefs.HasKey("Music"))
        {
            audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music", 0));
        }
        if (PlayerPrefs.HasKey("Efx"))
        {
            audioMixer.SetFloat("Efx", PlayerPrefs.GetFloat("Efx", 0));
        }
        // Quality:
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel", 1));
        }
    }
}
