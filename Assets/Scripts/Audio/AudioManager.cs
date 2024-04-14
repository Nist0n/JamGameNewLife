using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [Header("------------------Audio Source-----------------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource rainSource;
    [SerializeField] public AudioSource sfxSource;
    [Header("------------------Audio Clip-----------------")]

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
        Sound thunder = music[1];

        if (thunder != null)
        {
            musicSource.clip = thunder.audio;
            musicSource.Play();
        }

        Sound rain = music[2];

        if (rain != null)
        {
            rainSource.clip = rain.audio;
            rainSource.PlayDelayed(musicSource.clip.length);
        }
    }

    public void PlayMusic(string soundName)
    {
        rainSource.Stop();
        Sound s = music.Find(x => x.name == soundName);

        if (s != null)
        {
            musicSource.clip = s.audio;
            musicSource.Play();
        }
    }
    
    public void PlaySFX(string soundName)
    {
        Sound s = sounds.Find(x => x.name == soundName);

        if (s != null)
        {
            sfxSource.PlayOneShot(s.audio);
        }
    }
}
