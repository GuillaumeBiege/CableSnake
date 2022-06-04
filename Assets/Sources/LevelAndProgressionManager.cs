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


    public int totalNbLevels = 3;
}
