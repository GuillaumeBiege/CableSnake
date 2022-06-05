using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    //References
    [SerializeField] Button nextLevelButton = default;
    [SerializeField] Button quitButton = default;
    [SerializeField] Text scoreText = default;

    private void OnEnable()
    {
        nextLevelButton.onClick.AddListener(GoToNextLevel);
        quitButton.onClick.AddListener(GoBacktoMainMenu);
    }

    //private void OnDisable()
    //{
    //    //nextLevelButton.onClick.RemoveListener(GoToNextLevel);
    //    //quitButton.onClick.RemoveListener(GoBacktoMainMenu);
    //}

    public void Init()
    {
        scoreText.text = "Your score : " + GameManager.Instance.currentFoodNumber.ToString();
    }

    public void GoToNextLevel()
    {
        GameManager.Instance.GoToNextLevel();
    }

    public void GoBacktoMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
    }
}
