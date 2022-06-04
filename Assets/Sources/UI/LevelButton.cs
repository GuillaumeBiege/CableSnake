using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    //References
    [SerializeField] Button button = default;
    [SerializeField] Text text = default;
    [SerializeField] MainMenuManager mainMenu = default;

    //Variables
    [SerializeField] int LevelID = 1;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();

        mainMenu = FindObjectOfType<MainMenuManager>();

        button.onClick.AddListener(Click);
    }

    public void Init(int _id)
    {
        LevelID = _id;

        text.text = "Level " + _id.ToString();
    }

    public void Click()
    {
        mainMenu.AskToChangeScene(LevelID);
    }
}
