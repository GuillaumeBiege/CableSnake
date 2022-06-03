using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCounter : MonoBehaviour
{
    //References
    [SerializeField] Text text = default;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = GameManager.Instance.currentFoodNumber.ToString();
    }
}
