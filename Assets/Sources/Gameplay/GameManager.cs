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
    }
    #endregion

    
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }

    #region SceneManagement

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        int nextLevelID = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelID <= LevelAndProgressionManager.Instance.totalNbLevels)
        {
            SceneManager.LoadScene(nextLevelID);
        }
        else
        {
            GoToMainMenu();
        }
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
