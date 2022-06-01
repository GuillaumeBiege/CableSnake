using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //References

    //Variables


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
    }

    public void ObstacleCollision()
    {
        Debug.LogWarning("You Lose !");
    }


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
