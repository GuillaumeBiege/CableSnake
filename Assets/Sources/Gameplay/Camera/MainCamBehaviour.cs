using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamBehaviour : MonoBehaviour
{
    //References
    [SerializeField] Transform playerCamTraget = default;
    [SerializeField] Transform playerCamFocus = default;

    //Variables
    [SerializeField] float closingSpeed = 2f;
    [SerializeField] float stopDistance = 0.1f;


    private void LateUpdate()
    {
        //Cam pos follow
        Vector3 movementAxis = playerCamTraget.position - transform.position;
        if (movementAxis.magnitude > stopDistance)
        {
            Vector3 move = movementAxis.normalized * closingSpeed * movementAxis.magnitude * Time.deltaTime;
            transform.position = transform.position + move;
        }


        //Cam focus follow
        Vector3 toFocusAxis = playerCamFocus.position - transform.position;
        transform.forward = toFocusAxis.normalized;

    }

}
