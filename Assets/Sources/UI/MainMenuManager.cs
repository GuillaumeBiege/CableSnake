using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //References
    [Header("References")]
    [SerializeField] GameObject mainPanel = default;

    [Header("EntranceMenu")]
    [SerializeField] Button entranceConfirmation = default;

    [Header("MainMenu")]
    [SerializeField] GameObject MainMenu = default;

    [Header("LevelListMenu")]
    [SerializeField] GameObject LevelListMenu = default;

    Animator animator = default;




    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void AskToChangeScene(int _id)
    {
        SceneManager.LoadScene(_id);
    }


    public void SlideFromEntranceToMain()
    {
        animator.SetTrigger("EntranceToMain");
    }

    public void SlideFromMainToLevel()
    {
        animator.SetTrigger("MainToLevel");
    }

    public void SlideFromLevelToMain()
    {
        animator.SetTrigger("LevelToMain");
    }
}
