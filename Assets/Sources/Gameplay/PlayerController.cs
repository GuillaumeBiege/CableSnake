using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //References
    [Header("References")]
    [SerializeField] GameObject playerSphere = default;

    //Variables
    [Space(10), Header("Variables")]
    [SerializeField] float maxRotSpeed = 2f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] bool ControlsEnable = true;

        //Double click
    float lastClickTime = 0f;
    [SerializeField] float doubleClickTolerance = 0.2f;

    private void Awake()
    {
        playerSphere = GetComponentInChildren<PlayerSphereDetector>().gameObject;
    }


    private void Update()
    {
        
        

        //Jump controls
        if (Input.GetMouseButtonDown(0) && ControlsEnable)
        {
            //float timeSinceLastClick = Time.time - lastClickTime;

            //if (timeSinceLastClick <= doubleClickTolerance)
            //{
            //    Debug.Log("Jump !");

            //    StartCoroutine(JumpPlayerSphere());
            //}

            //lastClickTime = Time.time;

            float mousePosNormY = Input.mousePosition.y / Screen.height;
            if (mousePosNormY <= 0.15f)
            {
                StartCoroutine(JumpPlayerSphere());
                return;
            }
        }

        //Slide movement controls
        if (Input.GetMouseButton(0) && ControlsEnable)
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
        }
    }

    IEnumerator JumpPlayerSphere()
    {
        ControlsEnable = false;

        float sphereInitPosition = playerSphere.transform.localPosition.y;

        float timer = 0f;

        while (timer < 1f)
        {
            yield return null;
            timer += Time.deltaTime / 1f;

            //Range check
            timer = (timer > 1f) ? 1f : timer;

            float parabolaY = -4 * jumpHeight * timer * timer + 4 * jumpHeight * timer;

            //Jump go up
            playerSphere.transform.localPosition = new Vector3(0f, sphereInitPosition + parabolaY, 0f);
        }


        ControlsEnable = true;
    }


    //Recieve collision information form the visual and apply proper behaviour
    public void ProcessCollision(GameObject other)
    {
        ObstacleBehaviour obstacle = other.GetComponent<ObstacleBehaviour>();
        if (obstacle != null)
        {
            GameManager.Instance.ObstacleCollision();
        }
    }

   
}
