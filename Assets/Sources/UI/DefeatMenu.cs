using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatMenu : MonoBehaviour
{
    //References
    [SerializeField] Button RetryButton = default; 
    [SerializeField] Button QuitButton = default;



    private void OnEnable()
    {
        RetryButton.onClick.AddListener(ReloadCurrentScene);
        QuitButton.onClick.AddListener(GoBacktoMainMenu);
    }

    //private void OnDisable()
    //{
    //    RetryButton.onClick.RemoveListener(ReloadCurrentScene);
    //    QuitButton.onClick.RemoveListener(GoBacktoMainMenu);
    //}

    public void ReloadCurrentScene()
    {
        GameManager.Instance.ReloadCurrentScene();
    }

    public void GoBacktoMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
    }
}
