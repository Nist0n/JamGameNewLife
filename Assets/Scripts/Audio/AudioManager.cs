using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("------------------Audio Source-----------------")]
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _SFXSource;
    [Header("------------------Audio Clip-----------------")]
    public AudioClip _backgroundRain;
    public AudioClip _button;

    private void Start()
    {
        _musicSource.clip = _backgroundRain;
        _musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip);
    }
}
