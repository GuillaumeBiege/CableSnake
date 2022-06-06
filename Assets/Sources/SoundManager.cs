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
    [SerializeField] AudioMixerGroup MusicGroup = null;
    [SerializeField] AudioMixerGroup SFXGroup = null;
    AudioSource[] audioSources;

    AudioSource musicSource = default;

    //Variables
    [SerializeField] int NbSource = 10;
   

    [SerializeField] SoundBank bank = default;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        instance = this;

        audioSources = new AudioSource[NbSource];

        for (int i = 0; i < NbSource; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].outputAudioMixerGroup = SFXGroup;
        }

        bank = Resources.Load<SoundBank>("SoundBank");

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = MusicGroup;
        musicSource.volume = 0.5f;

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

    public void PlayMusic(AudioClip _clip)
    {
        musicSource.clip = _clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
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

    //Music

    public void PlayMusicGame()
    {
        PlayMusic(bank.MusicGameClip);
    }

    public void PlayMusicMenu()
    {
        PlayMusic(bank.MusicMenuClip);
    }

    //SFX

    public void PlayFoodSound()
    {
        PlaySound(bank.FoodClip);
    }

    public void PlayCollisionSound()
    {
        PlaySound(bank.CollisionClip);
    }

    public void PlayVictory()
    {
        PlaySound(bank.VictoryClip);
    }

    public void PlayDefeat()
    {
        PlaySound(bank.DefeatClip);
    }

    public void PlayInterfaceSound()
    {
        PlaySound(bank.InterfaceClip);
    }
}
