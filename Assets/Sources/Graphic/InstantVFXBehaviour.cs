using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantVFXBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] float lifetime = 1f;

    private void Start()
    {
        Invoke("Kill", lifetime);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
