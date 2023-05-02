using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace manager
{

    /// <summary>
    /// 배경, 이펙트, 사운드 개수
    /// </summary>
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public class SoundManager : MonoBehaviour
    {
        AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
        Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        public void Init()
        {
            GameObject root = GameObject.Find("@Sound");
            if(root == null)
            {
                root = new GameObject { name = "@Sound" };
                Object.DontDestroyOnLoad(root);

                string[] SoundNames = System.Enum.GetNames(typeof(Sound));
                for(int i = 0; i < SoundNames.Length - 1; i++)
                {
                    GameObject go = new GameObject { name = SoundNames[i] };
                    _audioSources[i] = go.AddComponent<AudioSource>();
                    go.transform.parent = root.transform;
                }

                _audioSources[(int)Sound.Bgm].loop = true;
            }
        }

        public void Clear()
        {
            foreach (AudioSource audioSource in _audioSources)
            {
                audioSource.clip = null;
                audioSource.Stop();
            }

            _audioClips.Clear();
        }

        public void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f)
        {
            if (audioClip == null)
                return;

            if(type == Sound.Bgm) //BGM 배경음악 재생
            {
                AudioSource audioSource = _audioSources[(int)Sound.Bgm];
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else //Effect 효과음 재생
            {
                AudioSource audioSource = _audioSources[(int)Sound.Effect];
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
            }
        }

        public void Play(string path, Sound type = Sound.Effect, float pitch = 1.0f)
        {
            AudioClip audioClip = GetOrAddAudioClip(path, type);
            Play(audioClip, type, pitch);
        }

        AudioClip GetOrAddAudioClip(string path, Sound type = Sound.Effect)
        {
            if (path.Contains("Sound/") == false)
                path = $"Sound/{path}";

            AudioClip audioClip = null;

            if(type == Sound.Bgm)
            {
                
            }
            else
            {
                if(_audioClips.TryGetValue(path, out audioClip) == false)
                {
                    
                }
            }
        }
    }
}
