using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void VoidDelegate();

public class GameManager : MonoBehaviour
{
    #region SingletonPattern
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return instance = new GameObject("@GameManager").AddComponent<GameManager>();
            }
        }
    }
    #endregion

    public enum GameState
    {
        INGAME,
        VICTORY,
        DEFEAT
    }

    //References

    //Variables
    [SerializeField] int currentFoodNumber = 1;
    [SerializeField] GameState currentGameState = GameState.INGAME;

    //Event
    public event VoidDelegate ONGameMode;
    public event VoidDelegate ONVictoryMode;
    public event VoidDelegate ONDefeatMode;


    private void Start()
    {
        
    }

    private void Update()
    {
        /////////////////DEBUG
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ProgressivlyStopTime(2f));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ResetTimeScale();
        }
        /////////////////DEBUG


        CheckVictoryDefeatCondition();
    }

    void CheckVictoryDefeatCondition()
    {
        //Defeat conditions
        if (currentFoodNumber <= 0)
        {
            currentFoodNumber = 0;

            ChangeGameState(GameState.DEFEAT);
        }

        
    }

    void ChangeGameState(GameState gs)
    {
        currentGameState = gs;

        switch (gs)
        {
            case GameState.INGAME:
                ONGameMode?.Invoke();
                break;
            case GameState.VICTORY:
                ONVictoryMode?.Invoke();
                break;
            case GameState.DEFEAT:
                ONDefeatMode?.Invoke();
                break;
            default:
                break;
        }
    }



    #region Collision management
    public void ObstacleCollision()
    {
        Debug.LogWarning("You Lose !");
        currentFoodNumber -= 3;
    }

    public void FoodCollision()
    {
        Debug.LogWarning("You got food !");
        currentFoodNumber++;
    }
    #endregion

    #region Time scale effects

    IEnumerator ProgressivlyStopTime(float _slowTime)
    {
        float timer = 1f;

        while (timer > 0f)
        {
            yield return null;

            timer -= Time.unscaledDeltaTime / _slowTime;

            Debug.Log("timer = " + timer.ToString());

            //To avoid any bug within Unity's manager
            timer = (timer < 0f) ? 0f : timer;

            Time.timeScale = timer;

        }
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    #endregion


    #region SceneManagement

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
