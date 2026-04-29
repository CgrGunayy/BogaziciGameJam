using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuSoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Awake()
    {
        SetSlider();
    }

    private void SetSlider()
    {
        mixer.SetFloat("MasterVol", 0.75f);
        mixer.SetFloat("MusicVol", 0.75f);
        mixer.SetFloat("SfxVol", 0.75f);

        masterVolumeSlider.value = 0.75f;
        musicVolumeSlider.value = 0.75f;  
        sfxVolumeSlider.value = 0.75f;
    }

    public void ChangeMainVolume(float value)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(value) * 20);
    }

    public void ChangeSFXVolume(float value)
    {
        mixer.SetFloat("SfxVol", Mathf.Log10(value) * 20);
    }

    public void ChangeMusicVolume(float value)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(value) * 20);
    }
}

