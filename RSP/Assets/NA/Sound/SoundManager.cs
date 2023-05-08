using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private Sound[] BGMClip, SFXClip;
    [SerializeField] private AudioSource BGMPlayer, SFXPlayer;

    /*Dictionary<string, AudioClip> BGMDic = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> SFXDic = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        BGMPlayer = GameObject.Find("BGMPlayer").GetComponent<AudioSource>();
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();

        foreach (Sound Bclip in BGMClip)
        {
            BGMDic.Add(Bclip.name, Bclip);
        }

        foreach (Sound clip in SFXClip)
        {

        }
    }*/

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

    public void Start()
    {
        BGMPlay("Theme");
    }

    public void BGMPlay(string name)
    {
        Sound s = Array.Find(BGMClip, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            BGMPlayer.clip = s.clip;
            BGMPlayer.Play();
        }
    }

    public void SFXPlay(string name)
    {
        Sound s = Array.Find(BGMClip, x => x.name == name);

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