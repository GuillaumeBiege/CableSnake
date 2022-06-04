using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void VoidDelegate();
public delegate void IntDelegate(int i);

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
        START,
        INGAME,
        VICTORY,
        DEFEAT
    }

    //References

    //Variables
    public int currentFoodNumber = 1;
    public GameState currentGameState = GameState.START;

    //Event
    public event VoidDelegate ONGameMode;
    public event VoidDelegate ONVictoryMode;
    public event VoidDelegate ONDefeatMode;

    public event IntDelegate ONIncreaseFood;
    public event IntDelegate ONDecreaseFood;

    private void Start()
    {
        Time.timeScale = 1f;
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

            StartCoroutine(ProgressivlyStopTime(0.2f));
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

    public void StartGame()
    {
        ChangeGameState(GameState.INGAME);
    }


    #region Collision management
    public bool ObstacleCollision()
    {
        bool check = (currentFoodNumber > 3) ? true : false;
        Debug.LogWarning("You Lose !");
        currentFoodNumber -= 3;
        ONDecreaseFood?.Invoke(3);

        return check;
    }

    public void FoodCollision()
    {
        Debug.LogWarning("You got food !");
        currentFoodNumber++;
        ONIncreaseFood?.Invoke(1);
    }

    public void FinishLineCollision()
    {
        Debug.LogWarning("You win !");
        ChangeGameState(GameState.VICTORY);
        StartCoroutine(ProgressivlyStopTime(1.5f));
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
