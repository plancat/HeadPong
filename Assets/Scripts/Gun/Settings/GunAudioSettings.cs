using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunAudioSettings
{
    [Header("Audio Source")]
    public AudioSource mainAudioSource;

    [Header("Audio Clips")]
    public AudioClip shootSound;
    public AudioClip reloadSound;
}