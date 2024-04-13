using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [Header("------------------Audio Source-----------------")]
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _SFXSource;
    [Header("------------------Audio Clip-----------------")]
    public AudioClip _backgroundRain;
    public AudioClip _button;

    public List<Sound> music, sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        PlayMusic("Main Menu Music 1");
    }

    public void ChangeMainMenuMusic()
    {
        PlayMusic("Main Menu Music 2");
    }

    public void PlayMusic(string soundName)
    {
        Sound s = music.Find(x => x.name == soundName);

        if (s != null)
        {
            _musicSource.clip = s.audio;
            _musicSource.Play();
        }
    }
    
    public void PlaySFX(string soundName)
    {
        Sound s = music.Find(x => x.name == soundName);

        if (s != null)
        {
            _SFXSource.PlayOneShot(s.audio);
        }
    }
}
