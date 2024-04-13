using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------------Audio Source-----------------")]
    [SerializeField] AudioSource _musicSorce;
    [SerializeField] AudioSource _SFXSorce;
    [Header("------------------Audio Clip-----------------")]
    public AudioClip backgroundSun;
    public AudioClip backgroundRain;
    private void Start()
    {
        _musicSorce.clip = backgroundSun;
        _musicSorce.Play();
    }
}
