using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager AudioInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    }

    private AudioSource BGMPlayer;
    private AudioSource SFXPlayer;

    [SerializeField] private AudioClip[] BGMClip;
    [SerializeField] private AudioClip[] SFXClip;

    public float VolumeSFX = 1.0f;
    public float VolumeBGM = 1.0f;

    Dictionary<string, AudioClip> audioclipDic = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if(AudioInstance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

/*        GameObject BS = new GameObject { name = "BGMSoundPlayer" };
        GameObject SS = new GameObject { name = "SFXSoundPlayer" };
*/
        BGMPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>();
        SFXPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();

        //BGM과 SFX Sound를 모두 DIctionary에 저장
        foreach (AudioClip audioClip in BGMClip)
        {
            audioclipDic.Add(audioClip.name, audioClip);
        }
        foreach (AudioClip audioClip in SFXClip)
        {
            audioclipDic.Add(audioClip.name, audioClip);
        }
    }

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