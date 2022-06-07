using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAndProgressionManager : MonoBehaviour
{

    #region SingletonPattern
    private static LevelAndProgressionManager instance = null;

    public static LevelAndProgressionManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return instance = new GameObject("@LevelAndProgressionManager").AddComponent<LevelAndProgressionManager>();
            }
        }
    }
    #endregion

    public struct LevelScore
    {
        public int LevelID;
        public int Score;
        public bool IsLevelComplet;

    }


    public int totalNbLevels = 5;

    public LevelScore[] levelScores = default;

    [SerializeField] string savingNameScore = "CableSnake_LevelScore_";
    [SerializeField] string savingNameComplet = "CableSnake_LevelComplet_";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        levelScores = new LevelScore[totalNbLevels];

        //Proper init
        for (int i = 0; i < totalNbLevels; i++)
        {
            levelScores[i].LevelID = i + 1;
            levelScores[i].Score = 0;
            levelScores[i].IsLevelComplet = false;
        }

        SaveToPref();

        LoadFromPref();
    }

    public int GetLevelScore(int _levelID)
    {
        if (_levelID >= 0 && _levelID < levelScores.Length)
            return levelScores[_levelID].Score;
        else
            return 0;
    }

    public bool GetLevelComplet(int _levelID)
    {
        if (_levelID >= 0 && _levelID < levelScores.Length)
            return levelScores[_levelID].IsLevelComplet;
        else
            return false;
    }

    void LoadFromPref()
    {
        for (int i = 0; i < totalNbLevels; i++)
        {
            levelScores[i].LevelID = i + 1;
            levelScores[i].Score = PlayerPrefs.GetInt(savingNameScore + (i + 1).ToString());
            levelScores[i].IsLevelComplet = (PlayerPrefs.GetInt(savingNameComplet + (i + 1).ToString()) == 1) ? true : false;
        }
    }

    void SaveToPref()
    {
        for (int i = 0; i < totalNbLevels; i++)
        {
            PlayerPrefs.SetInt(savingNameScore + (i + 1).ToString(), levelScores[i].Score);
            PlayerPrefs.SetInt(savingNameComplet + (i + 1).ToString(), (levelScores[i].IsLevelComplet) ? 1 : 0);
        }
    }

    public void SaveHighScoreFromGame(int _levelID, int _score)
    {
        if(_score > levelScores[_levelID].Score)
            levelScores[_levelID].Score = _score;

        levelScores[_levelID].IsLevelComplet = true;

        SaveToPref();
    }
}
