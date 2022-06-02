using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAxisControl : MonoBehaviour
{
    [SerializeField] Transform playerTarget = default;
    [SerializeField] float transSpeed = 0.1f;
    [SerializeField] float rotSpeed = 0.1f;


    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, playerTarget.position, transSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerTarget.rotation, rotSpeed * Time.deltaTime);
    }
}
