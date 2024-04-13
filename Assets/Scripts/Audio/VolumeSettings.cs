using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _myMixer;
    [SerializeField] private Slider _Slider;
    private void Start()
    {
        SetAllVolume();
    }
    public void SetAllVolume()
    {
        float volume = _Slider.value;
        _myMixer.SetFloat("Volume", MathF.Log10(volume)*20);
    }
}
