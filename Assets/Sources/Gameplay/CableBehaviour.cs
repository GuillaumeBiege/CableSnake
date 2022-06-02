using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableBehaviour : MonoBehaviour
{
    //References
    Rigidbody rb = default;
    
    //Variables
    [SerializeField] float currentspeed = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        GameManager.Instance.ONGameMode += Init;
    }


    public void Init()
    {
        rb.velocity = -transform.forward * currentspeed;
    }
}
