using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //References
    [Header("References")]
    [SerializeField] GameObject playerSphere = default;
    [SerializeField] GameObject prefabSnakeTailSegment = default;
    [SerializeField] GameObject prefabFX_Victory = default;

    //Variables
    [Space(10), Header("Variables")]
    [SerializeField] float maxRotSpeed = 2f;
    public float jumpHeight = 3f;
    [SerializeField] bool ControlsEnable = false;

    [SerializeField] float snakeTailSgmentGap = 0.3f;
    [SerializeField] Stack<SnakeTailSegment> snakeTails = new Stack<SnakeTailSegment>();
    [SerializeField] int snakeTailVisualCap = 8;
    [SerializeField] float snakeTailJumpDelay = 0.05f;

        //Double click
    float lastClickTime = 0f;
    [SerializeField] float doubleClickTolerance = 0.2f;

    private void Awake()
    {
        playerSphere = GetComponentInChildren<PlayerSphereDetector>().gameObject;

        GameManager.Instance.ONIncreaseFood += AddSnakeTailSegments;
        GameManager.Instance.ONDecreaseFood += RemoveLastSegment;
        GameManager.Instance.ONGameMode += EnableControls;
        GameManager.Instance.ONDefeatMode += DisableControls;
        GameManager.Instance.ONVictoryMode += DisableControls;
    }


    private void Update()
    {
        
        //Jump controls
        if (Input.GetMouseButtonDown(0) && ControlsEnable)
        {
            float mousePosNormY = Input.mousePosition.y / Screen.height;
            if (mousePosNormY <= 0.2f)
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
                transform.Rotate(Vector3.forward, maxRotSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.forward, -maxRotSpeed * Time.deltaTime);
            }
        }

        

    }

    IEnumerator JumpPlayerSphere()
    {
        ControlsEnable = false;

        //Tail jump
        SnakeTailSegment[] tails = snakeTails.ToArray();
        int y = 0;
        for (int i = tails.Length -1; i >= 0 ; i--)
        {
            tails[i].StartJump((y + 1) * snakeTailJumpDelay);
            y++;
        }

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
        //Process obstacles
        ObstacleBehaviour obstacle = other.GetComponent<ObstacleBehaviour>();
        if (obstacle != null)
        {
            if (GameManager.Instance.ObstacleCollision())   //If the player survive the collision it is the obstacle that is destroyed
                obstacle.Kill();

            return;
        }

        //Process food
        FoodBehaviour food = other.GetComponent<FoodBehaviour>();
        if (food != null)
        {
            GameManager.Instance.FoodCollision();
            food.KillItself();
            return;
        }

        //Victory
        FinishLineBehaviour finishLine = other.GetComponent<FinishLineBehaviour>();
        if (finishLine != null)
        {
            GameManager.Instance.FinishLineCollision();

            GameObject go = Instantiate<GameObject>(prefabFX_Victory, transform);
            go.transform.position = playerSphere.transform.position;
            go.transform.rotation = playerSphere.transform.rotation;
            return;
        }
    }

    public void EnableControls()
    {
        ControlsEnable = true;
    }

    public void DisableControls()
    {
        ControlsEnable = false;
    }

    #region Snake tail segment management
    public void AddSnakeTailSegments(int nb)
    {
        if (snakeTails.Count < snakeTailVisualCap)
        {
            for (int i = 0; i < nb; i++)
            {
                GameObject go = Instantiate<GameObject>(prefabSnakeTailSegment);
                SnakeTailSegment tailSeg = go.GetComponent<SnakeTailSegment>();
                tailSeg.player = this;

                if (snakeTails.Count <= 0)
                {
                    go.transform.position = transform.position + new Vector3(0f, 0f, -snakeTailSgmentGap);
                    go.transform.rotation = transform.rotation;


                    tailSeg.previousSegment = transform;
                }
                else
                {
                    Transform lastSeg = snakeTails.Peek().transform;

                    go.transform.position = lastSeg.position + new Vector3(0f, 0f, -snakeTailSgmentGap);
                    go.transform.rotation = lastSeg.rotation;

                    tailSeg.previousSegment = lastSeg;
                }


                snakeTails.Push(tailSeg);
            }
        }
    }

    public void RemoveLastSegment(int nb)
    {
        for (int i = 0; i < nb && snakeTails.Count > 0; i++)
        {
            Destroy(snakeTails.Pop().gameObject);
        }
    }
    #endregion


}
