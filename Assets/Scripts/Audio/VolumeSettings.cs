using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _myMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
        }
    }
    
    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _myMixer.SetFloat("music", MathF.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void SetSfxVolume()
    {
        float volume = _SFXSlider.value;
        _myMixer.SetFloat("SFX", MathF.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    
    public void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume");
        _musicSlider.value = musicVolume;
        
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        _SFXSlider.value = sfxVolume;
    }
}
