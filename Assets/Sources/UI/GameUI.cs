using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    //References
    [SerializeField] DefeatMenu defeatMenu = default;
    [SerializeField] VictoryMenu victoryMenu = default;

    private void Start()
    {
        //safety check
        defeatMenu.gameObject.SetActive(false);
        victoryMenu.gameObject.SetActive(false);

        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusicGame();

    }

    private void OnEnable()
    {
        GameManager.Instance.ONDefeatMode += InitDefeatMenu;
        GameManager.Instance.ONVictoryMode += InitVictoryMenu;
    }

    //private void OnDisable()
    //{
    //    GameManager.Instance.ONDefeatMode -= InitDefeatMenu;
    //    GameManager.Instance.ONVictoryMode -= InitVictoryMenu;
    //}

    public void InitDefeatMenu()
    {
        defeatMenu.gameObject.SetActive(true);
    }
    
    public void InitVictoryMenu()
    {
        victoryMenu.gameObject.SetActive(true);
        victoryMenu.Init();
    }
}
