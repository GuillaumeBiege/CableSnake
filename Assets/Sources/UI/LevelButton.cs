using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    //References
    [SerializeField] Button button = default;
    [SerializeField] Text text = default;
    [SerializeField] Text textScore = default;
    [SerializeField] MainMenuManager mainMenu = default;

    //Variables
    [SerializeField] int LevelID = 1;

    private void Awake()
    {
        button = GetComponent<Button>();

        mainMenu = FindObjectOfType<MainMenuManager>();

        button.onClick.AddListener(Click);
    }

    public void Init(int _id)
    {
        LevelID = _id;

        text.text = "Level " + _id.ToString();
        textScore.text = "Best score : " + LevelAndProgressionManager.Instance.GetLevelScore(LevelID).ToString();

        if(LevelID > 1)
        {
            if (LevelAndProgressionManager.Instance.GetLevelComplet(LevelID - 1))
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
            
    }

    public void Click()
    {
        mainMenu.AskToChangeScene(LevelID);
    }
}
