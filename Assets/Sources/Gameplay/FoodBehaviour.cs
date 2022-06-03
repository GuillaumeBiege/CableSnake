using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    //References
    [SerializeField] GameObject deathFX = default;
    [SerializeField] CableBehaviour cable = default;

    private void Start()
    {
        cable = GetComponentInParent<CableBehaviour>();
    }

    public void KillItself()
    {
        //Create FX at death
        GameObject go = Instantiate<GameObject>(deathFX, cable.transform);
        go.transform.position = transform.position;

        //Die
        Destroy(gameObject);
    }
}
