using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private Sound[] BGMClip, SFXClip;
    [SerializeField] private AudioSource BGMPlayer, SFXPlayer;
    [SerializeField] private Slider BGMSlider, SFXSlider;

    public float bSound, sSound;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BGMPlayer.volume = PlayerPrefs.GetFloat("Volume", 1f);
        BGMSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        BGMSlider.onValueChanged.AddListener(BGMVolume);

        SFXPlayer.volume = PlayerPrefs.GetFloat("Volume", 1f);
        SFXSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        SFXSlider.onValueChanged.AddListener(SFXVolume);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "1. Title" || arg0.name == "2. bracket")
        {
            BGMPlay("Theme");
        }
        else if (arg0.name == "3. GameScene")
        {
            BGMPlay("GameScene");
        }
    }

    public void BGMPlay(string name)
    {
        Sound s = Array.Find(BGMClip, x => x.name == name);

        BGMPlayer.clip = s.clip;
        BGMPlayer.loop = true;
        BGMPlayer.Play();
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

    public void BGMVolume(float volume)
    {
        BGMPlayer.volume = volume;
        bSound = volume;
        PlayerPrefs.SetFloat("Volume", BGMPlayer.volume);
    }

    public void SFXVolume(float volume)
    {
        SFXPlayer.volume = volume;
        sSound = volume;
        PlayerPrefs.SetFloat("Volume", SFXPlayer.volume);
    }
}