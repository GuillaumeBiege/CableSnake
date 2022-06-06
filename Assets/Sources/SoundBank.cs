using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundBank",menuName = "ScriptableObject")]
public class SoundBank : ScriptableObject
{
    public AudioClip MusicGameClip = default;
    public AudioClip MusicMenuClip = default;
    public AudioClip FoodClip = default;
    public AudioClip CollisionClip = default;
    public AudioClip VictoryClip = default;
    public AudioClip DefeatClip = default;
    public AudioClip InterfaceClip = default;

    public SoundBank()
    {

    }
}
