using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //References
    [Header("References")]
    [SerializeField] Button playButton = default;
    [SerializeField] Button playQuit = default;


    private void OnEnable()
    {
        playButton.onClick.AddListener(GoToGameScene);
        playQuit.onClick.AddListener(CloseGame);

    }

    
    private void OnDisable()
    {
        playButton.onClick.RemoveListener(GoToGameScene);
        playQuit.onClick.RemoveListener(CloseGame);
    }



    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
