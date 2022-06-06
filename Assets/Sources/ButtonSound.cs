using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] Button button = default;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(PlayButtonSound);
    }

    public void PlayButtonSound()
    {
        SoundManager.Instance.PlayInterfaceSound();
    }
}
