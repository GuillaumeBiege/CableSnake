using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableBehaviour : MonoBehaviour
{
    //References
    Rigidbody rb = default;
    
    //Variables
    [SerializeField] float currentspeed = 1f;
    [SerializeField] bool IsMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        GameManager.Instance.ONGameMode += Init;
    }


    public void Init()
    {
        IsMoving = true;
    }

    private void LateUpdate()
    {
        if (IsMoving)
            transform.position += -transform.forward * currentspeed * Time.deltaTime;
    }
}
