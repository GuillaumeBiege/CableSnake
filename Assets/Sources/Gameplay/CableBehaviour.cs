using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObstacleSegment
{
    public GameObject obstacle = null;
    public float angle = 0f;
}

[Serializable]
public class ObsoleteSegment
{
    public GameObject obstacle = null;
    public GameObject segment = null;
    public int index = 0;

    public ObsoleteSegment(GameObject obs, GameObject seg, int i)
    {
        obstacle = obs;
        segment = seg;
        index = i;
    }
}

public class CableBehaviour : MonoBehaviour
{
    //References
    Rigidbody rb = default;
    
    //Variables
    [SerializeField] float initialspeed = 5f;
    [SerializeField] float maxspeed = 15f;
    [SerializeField] float currentspeed = 1f;
    [SerializeField] bool IsMoving = false;


    [Header("LevelGeneration")]
    [SerializeField] GameObject cableSegment = default;
    [SerializeField] ObstacleSegment[] obstacleList = new ObstacleSegment[30];
    [SerializeField] int progressionIndex = 0;
    [SerializeField] int initialIndex = 5;
    [SerializeField] float progressionStep = 10f;
    [SerializeField] List<ObsoleteSegment> instancedObtacles = default;

    [SerializeField] float progressionTimer = 0f;

    [SerializeField] float slowDownTime = 2.5f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        GameManager.Instance.ONGameMode += LaunchProgression;
        GameManager.Instance.ONVictoryMode += StopCableProgessif;
        GameManager.Instance.ONDefeatMode += StopCable;
    }

    private void Start()
    {
        Init();
    }


    public void LaunchProgression()
    {
        IsMoving = true;
        currentspeed = initialspeed;
    }

    private void Update()
    {
        if (IsMoving)
            transform.position += -transform.forward * currentspeed * Time.deltaTime;

        GenerateSegmentOnTimer();
    }

    #region Level generation
    void Init()
    {
        
        //Initiate a few segment at the biggining of the game
        for (int i = 0; i < initialIndex && i < obstacleList.Length; i++)
        {
            AddFullSegment();
        }
    }

    void AddFullSegment()
    {
        if (progressionIndex < obstacleList.Length)
        {
            GameObject obstacle = null;
            GameObject cable = null;

            //Generate an obstacle if need be
            if (obstacleList[progressionIndex].obstacle != null)
            {
                obstacle = Instantiate<GameObject>(obstacleList[progressionIndex].obstacle, transform);
                obstacle.transform.localPosition = new Vector3(0f, 0f, (float)progressionIndex * progressionStep);
                obstacle.transform.localEulerAngles = new Vector3(0f, 0f, obstacleList[progressionIndex].angle);
            }

            //Generate the cable segment
            cable = Instantiate<GameObject>(cableSegment, transform);
            cable.transform.localPosition = new Vector3(0f, 0f, (float)progressionIndex * progressionStep);
            progressionIndex++;

            instancedObtacles.Add(new ObsoleteSegment(obstacle, cable, progressionIndex));

        }
    }

    //Check in the list of created segment and destroy old ones
    void RemovePastSegment()
    {
        for (int i = 0; i < instancedObtacles.Count; i++)
        {
            if (instancedObtacles[i].index <= progressionIndex - initialIndex - 2)
            {
                if(instancedObtacles[i].obstacle != null)
                    Destroy(instancedObtacles[i].obstacle);

                if (instancedObtacles[i].segment != null)
                    Destroy(instancedObtacles[i].segment);

                instancedObtacles.RemoveAt(i);
                break;
            }
        }
    }

    void GenerateSegmentOnTimer()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.INGAME)
        {
            float segmentTime = progressionStep / currentspeed;

            progressionTimer += Time.deltaTime;

            if (progressionTimer >= segmentTime)
            {
                progressionTimer -= segmentTime;


                AddFullSegment();

                RemovePastSegment();

                //Increase the progression speed according
                IncrementSpeed();
            }
        }
    }

    
    #endregion

    void IncrementSpeed()
    {
        int currentIndex = progressionIndex - initialIndex;
        int maxIndex = obstacleList.Length - initialIndex;                     //Total length of the level

        if (currentIndex >= 0)
        {
            Debug.Log("Speed !");
            currentspeed = Mathf.Lerp(initialspeed, maxspeed, ((float)currentIndex / (float)maxIndex));
        }
    }

    public void StopCableProgessif()
    {
        StartCoroutine(SlowDownCable());
    }

    public void StopCable()
    {
        currentspeed = 0f;
        IsMoving = false;
    }

    IEnumerator SlowDownCable()
    {
        float timer = 1f;

        while (timer > 0f)
        {
            yield return null;

            timer -= Time.deltaTime / slowDownTime;

            timer = (timer < 0f) ? 0f : timer;      //Bondaries

            currentspeed = Mathf.Lerp(0f, maxspeed, timer);
        }

        IsMoving = false;
    }
}
