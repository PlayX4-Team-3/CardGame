using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private Sound[] SFXClip;
    [SerializeField] private AudioSource BGMPlayer, SFXPlayer;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SFXPlay(string name)
    {
        Sound s = Array.Find(SFXClip, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            SFXPlayer.PlayOneShot(s.clip);
        }
    }

    public void ToggleBGM()
    {
        BGMPlayer.mute = !BGMPlayer.mute;
    }

    public void ToggleSFX()
    {
        SFXPlayer.mute = !SFXPlayer.mute;
    }

    public void BGMVolume(float volume)
    {
        BGMPlayer.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        SFXPlayer.volume = volume;
    }
}