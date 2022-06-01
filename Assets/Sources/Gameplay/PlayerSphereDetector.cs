using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSphereDetector : MonoBehaviour
{
    //References
    [SerializeField] PlayerController player = default;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.ProcessCollision(other.gameObject);
    }
}
