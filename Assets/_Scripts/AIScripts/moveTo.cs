using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveTo : MonoBehaviour
{
    //init vars
    public NavMeshAgent agent;
    private AISight selfSight;

    //states
    public bool stalking = false;
    public bool chasing = false;
    public bool searching = false;
    public bool suspended = false;

    //roaming route
    //public Transform[] pathPoints = new Transform[4];
   // public int pathIndex = 0;
    private Transform playerPos;
    private GameObject[] player;
    public Transform lastPlayerSight;
    private float timer;
    private bool timeAttack;
    private float randTime;

    //constants
    //public float stalkSpeed = 3f;
    public float chaseSpeed = 8f;
    public float searchSpeed = 5f;

    //init vars
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selfSight = GetComponent<AISight>();
        player = GameObject.FindGameObjectsWithTag("Player");//player reference
        playerPos = player[0].transform;
        lastPlayerSight = playerPos;
        timeAttack = false;
        randTime = Random.Range(60.0f, 120.0f);
        timer = randTime;
    }

    //state machine
    void Update()
    {
        timer -= Time.deltaTime;
        if(suspended)
        {
            Suspended();//halt all manner of evilness
        }
        if (selfSight.playerInSight == 1 || timer <= 0)//if the player is seen
        {
            if (timer <= 0)
            {
                timeAttack = true;
                Chasing();//chase them
            }
            else
                Chasing();
        }
        else if (selfSight.playerMissing == 1)//if they went missing while they were being chased
        {
            Searching();//search a bit
        }
        else//otherwise
        {
            Stalking();//keep stalking
        }
    }

    void Suspended()
    {
        stalking = false;
        chasing = false;
        searching = false;
        //agent.GetComponent<NavMeshAgent>().isStopped = true;
    }

    //chasing the player by running at them according to the navmesh
    void Chasing()
    {
        stalking = false;
        chasing = true;
        searching = false;
        agent.speed = chaseSpeed;
        agent.destination = playerPos.position;//chase the player
    }

    //searching for the player since they "disappeared"
    void Searching()
    {
        stalking = false;
        chasing = false;
        searching = true;
        agent.speed = searchSpeed;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            selfSight.playerMissing = -1;
        }
    }

    //stalking the player by standing at different locations based off of player position and staring at the player
    void Stalking()
    {
        stalking = true;
        chasing = false;
        searching = false;
        agent.speed = 0f;

        //reset timer iff bad man attacked due to timer == 0
        if(timeAttack == true)
        {
            randTime = Random.Range(60.0f, 120.0f);
            timer = randTime;
        }


        //see wayPointSight script for more

        //if (agent.remainingDistance <= agent.stoppingDistance)//if we have arrived at a checkpoint
        //{
        //    if (pathIndex < pathPoints.Length - 1)//move on to the next pathPoints.Length
        //    {
        //        pathIndex++;//increment

        //    }
        //    else
        //    {
        //        pathIndex = 0;//otherwise, reset to zero
        //    }
        //}
        ////print(pathIndex);
        //agent.destination = pathPoints[pathIndex].position;//continue stalk route
    }
}
