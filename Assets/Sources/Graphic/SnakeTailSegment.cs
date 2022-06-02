using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTailSegment : MonoBehaviour
{
    //References
    public Transform previousSegment = default;
    [SerializeField] GameObject segmentSphere = default;
    public PlayerController player = default;


    //Variables
    [SerializeField] float rotSpeed = 0.1f;




    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, previousSegment.rotation, rotSpeed * Time.deltaTime);
    }

    public void StartJump(float delay)
    {
        StartCoroutine(JumpPlayerSphere(delay));
    }

    IEnumerator JumpPlayerSphere(float delay)
    {
        yield return new WaitForSeconds(delay);

        float sphereInitPosition = segmentSphere.transform.localPosition.y;

        float timer = 0f;

        while (timer < 1f)
        {
            yield return null;
            timer += Time.deltaTime / 1f;

            //Range check
            timer = (timer > 1f) ? 1f : timer;

            float parabolaY = -4f * player.jumpHeight * timer * timer + 4f * player.jumpHeight * timer;

            //Jump go up
            segmentSphere.transform.localPosition = new Vector3(0f, sphereInitPosition + parabolaY, 0f);
        }


    }
}
