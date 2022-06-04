using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    //References
    [SerializeField] GameObject levelViewport = default;

    [SerializeField] GameObject prefabLevelButton = default;


    private void Start()
    {
        int totLevels = LevelAndProgressionManager.Instance.totalNbLevels;

        for (int i = 0; i < totLevels; i++)
        {
            GameObject go = Instantiate<GameObject>(prefabLevelButton, levelViewport.transform);
            go.name = "ButtonLevel" + i.ToString();

            LevelButton button = go.GetComponent<LevelButton>();
            if (button != null)
                button.Init(i + 1);
        }
    }
}
