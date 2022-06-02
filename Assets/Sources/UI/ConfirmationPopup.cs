using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    //References
    [SerializeField] Button confirmationButton = default;

    private void Awake()
    {
        confirmationButton = GetComponent<Button>();

        confirmationButton.onClick.AddListener(PressConfirmation);
    }

    public void PressConfirmation()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }
    
}
