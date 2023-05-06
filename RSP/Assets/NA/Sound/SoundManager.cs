using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource BGMPlayer;
    private AudioSource SFXPlayer;

    [SerializeField] private AudioClip[] BGMClip;
    [SerializeField] private AudioClip[] SFXClip;

    public float VolumeSFX = 1.0f;
    public float VolumeBGM = 1.0f;

    Dictionary<string, AudioClip> audioclipDic = new Dictionary<string, AudioClip>();

    public void BGMPlay(string name, float volume = 1.0f)
    {
        BGMPlayer.loop = true;
        BGMPlayer.volume = volume * VolumeBGM;
    }
    public void SFXPLay(string name, float volume = 1.0f)
    {
        SFXPlayer.PlayOneShot(audioclipDic[name], volume * VolumeSFX);
    }

}