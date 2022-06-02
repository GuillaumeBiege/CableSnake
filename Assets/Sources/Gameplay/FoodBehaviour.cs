using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    //References
    [SerializeField] GameObject deathFX = default;


    public void KillItself()
    {
        //Create FX at death
        GameObject go = Instantiate<GameObject>(deathFX);
        go.transform.position = transform.position;

        //Die
        Destroy(gameObject);
    }
}
