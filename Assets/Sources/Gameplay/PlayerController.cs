using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //References


    //Variables
    [SerializeField] float maxRotSpeed = 2f;


    private void Update()
    {
        //Controls
        if (Input.GetMouseButton(0))
        {
            float mousePosNorm = Input.mousePosition.x / Screen.width;

            if (mousePosNorm < 0.5f)
            {
                transform.Rotate(Vector3.forward, maxRotSpeed);
            }
            else
            {
                transform.Rotate(Vector3.forward, -maxRotSpeed);
            }
            

            //Debug.Log("Mouse % = " + transformedNorm.ToString());
        }
    }
}
