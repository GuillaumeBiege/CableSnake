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

    //References
    [SerializeField] AudioMixerGroup audioMixerGroup = null;
    AudioSource[] audioSources;


    //Variables
    [SerializeField] int NbSource = 10;
    


    [Header("Audio clip")]
    [SerializeField] AudioClip MusicGameClip = default;
    [SerializeField] AudioClip MusicMenuClip = default;
    [SerializeField] AudioClip FoodClip = default;
    [SerializeField] AudioClip CollisionClip = default;
    [SerializeField] AudioClip VictoryClip = default;
    [SerializeField] AudioClip DefeatClip = default;
    [SerializeField] AudioClip InterfaceClip = default;


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

    public void PlaySound(AudioClip _clip, bool _isOnLoop = false)
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


    public void PlayMusicGame()
    {
        PlaySound(MusicGameClip, true);
    }

    public void StopMusicGame()
    {
        StopSound(MusicGameClip);
    }

    public void PlayMusicMenu()
    {
        PlaySound(MusicMenuClip, true);
    }

    public void StopMusicMenu()
    {
        StopSound(MusicMenuClip);
    }

    public void PlayFoodSound()
    {
        PlaySound(FoodClip);
    }

    public void PlayCollisionSound()
    {
        PlaySound(CollisionClip);
    }

    public void PlayVictory()
    {
        PlaySound(VictoryClip);
    }

    public void PlayDefeat()
    {
        PlaySound(DefeatClip);
    }

    public void PlayInterfaceSound()
    {
        PlaySound(InterfaceClip);
    }
}
