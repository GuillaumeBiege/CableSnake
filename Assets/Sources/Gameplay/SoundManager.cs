using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region SingletonPattern
    private static SoundManager instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return instance = new GameObject("@SoundManager").AddComponent<SoundManager>();
            }
        }
    }
    #endregion

    [SerializeField] AudioMixerGroup audioMixerGroup = null;
    [SerializeField] int NbSource = 10;
    AudioSource[] audioSources;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        instance = this;

        audioSources = new AudioSource[NbSource];

        for (int i = 0; i < NbSource; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].outputAudioMixerGroup = audioMixerGroup;
        }
    }

    public void PlaySound(AudioClip _clip, bool _isOnLoop)
    {
        for (int i = 0; i < NbSource; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].clip = _clip;
                audioSources[i].loop = _isOnLoop;
                audioSources[i].Play();
                break;
            }
        }
    }

    public void StopSound(AudioClip _clip)
    {
        for (int i = 0; i < NbSource; i++)
        {
            if (audioSources[i].clip == _clip && audioSources[i].isPlaying)
            {
                audioSources[i].Stop();
                audioSources[i].loop = false;
            }
        }
    }

    public void StopAllSound()
    {
        for (int i = 0; i < NbSource; i++)
        {
            if (audioSources[i].isPlaying)
            {
                audioSources[i].Stop();
            }
        }
    }
}
