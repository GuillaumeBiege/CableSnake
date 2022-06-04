using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    //Variables
    [SerializeField] float rotSpeed = 20f;
    [SerializeField] bool IsClockwise = true;

    private void Update()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.INGAME)
        {
            transform.Rotate(Vector3.forward, ((IsClockwise) ? -1 : 1) * rotSpeed * Time.deltaTime);
        }
    }
}
