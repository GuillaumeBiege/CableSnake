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


    [Header("LevelGeneration")]
    [SerializeField] GameObject cableSegment = default;
    [SerializeField] GameObject[] obstacleList = new GameObject[30];
    [SerializeField] int progressionIndex = 0;
    [SerializeField] float progressionStep = 10f;
    [SerializeField] Queue<GameObject> instancedObtacles = default;

    [SerializeField] float progressionTimer = 0f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        GameManager.Instance.ONGameMode += LaunchProgression;
    }

    private void Start()
    {
        Init();
    }


    public void LaunchProgression()
    {
        IsMoving = true;
    }

    private void LateUpdate()
    {
        if (IsMoving)
            transform.position += -transform.forward * currentspeed * Time.deltaTime;
    }

    #region Level generation
    void Init()
    {
        //For the player breathing room at the beggining of a level
        //AddEmptySegment();


        for (int i = 0; i < 6 && i < obstacleList.Length; i++)
        {
            AddFullSegment();


            
        }
    }

    void AddEmptySegment()
    {
        GameObject cable = Instantiate<GameObject>(cableSegment, transform);
        cable.transform.position = new Vector3(0f, 0f, progressionIndex * progressionStep);
        progressionIndex++;
    }

    void AddFullSegment()
    {
        if (progressionIndex < obstacleList.Length)
        {
            GameObject obstacle = Instantiate<GameObject>(obstacleList[progressionIndex], transform);
            obstacle.transform.position = new Vector3(0f, 0f, progressionIndex * progressionStep);

            AddEmptySegment();
        }
    }


    private void Update()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.INGAME)
        {
            float segmentTime = progressionStep / currentspeed;

            progressionTimer += Time.deltaTime;

            if (progressionTimer >= segmentTime)
            {
                progressionTimer -= segmentTime;
                
                
                AddFullSegment();
            }
        }
        
    }
    #endregion


}
