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
    public bool returning = false;

    //roaming route
    //public Transform[] pathPoints = new Transform[4];
   // public int pathIndex = 0;
    private Transform playerPos;
    private GameObject[] player;
    public Transform lastPlayerSight;
    private float timer;
    private bool timeAttack;
    private float randTime;
    private bool startOffset;
    //public bool caught;

    //constants
    //public float stalkSpeed = 3f;
    public float chaseSpeed = 8f;
    public float searchSpeed = 5f;
    private float offsetTime = 2f;

    //animation states
    private float idle = 0.0f;
    private float run = 1.0f;
    private float walk = 0.5f;

    private Animator anim;

    public GameObject stalkObj;

    //init vars
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selfSight = GetComponent<AISight>();
        player = GameObject.FindGameObjectsWithTag("Player");//player reference
        playerPos = player[0].transform;
        lastPlayerSight = playerPos;
        timeAttack = false;
        randTime = Random.Range(5.0f, 6.0f);
        timer = randTime;
        anim = gameObject.GetComponentInChildren<Animator>();
        startOffset = false;
        stalkObj = null;
        //caught = false;
        //offsetTime = 2f;
    }

    //state machine
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if(startOffset)
        {
            offsetTime -= Time.deltaTime;
        }

        if(suspended)
        {
            Suspended();//halt all manner of evilness
        }
        else if (selfSight.playerInSight == 1 || timer <= 0)//if the player is seen   // !Player.IsHiding && 
        {
            //print(selfSight.playerInSight);
            if (timer <= 0)
            {
                timeAttack = true;
                Chasing();//chase them
            }
            else
                Chasing();
        }
        else if ((Player.IsHiding && selfSight.playerMissing == 1) || searching)//if they went missing while they were being chased //|| selfSight.behindWall
        {
            Searching();//search a bit
        }
        else if(returning)//&& selfSight.playerInSight == -1
        {
            Returning();
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

        agent.speed = 0;
        anim.SetFloat("Speed_f", idle);
    }

    //chasing the player by running at them according to the navmesh
    void Chasing()
    {
        
        //reset timer iff bad man attacked due to timer == 0
        if (timeAttack == true)
        {
            randTime = Random.Range(60.0f, 120.0f);
            timer = randTime;
            timeAttack = false;
        }

        stalking = false;
        chasing = true;
        searching = false;
        returning = false;
        agent.speed = chaseSpeed;

        agent.destination = playerPos.position;//chase the player
        

        //print(Mathf.Abs(agent.remainingDistance - agent.stoppingDistance) <= 5);
        anim.SetFloat("Speed_f", run);

        Player.LightController.IsFlickering = true;
        

    }

    //searching for the player since they "disappeared"
    void Searching()
    {
        //float offsetTime = 2f;
        startOffset = true;
        //reset timer iff bad man attacked due to timer == 0
        if (timeAttack == true)
        {
            randTime = Random.Range(60.0f, 120.0f);
            timer = randTime;
            timeAttack = false;
        }

        stalking = false;
        chasing = false;
        searching = true;
        returning = false;
        agent.speed = searchSpeed;
        //print(Mathf.Abs(agent.remainingDistance - agent.stoppingDistance) <= 5);
        //print(offsetTime);
        
        if (!agent.pathPending && (Mathf.Abs(agent.remainingDistance - agent.stoppingDistance) <= 5 || selfSight.playerInSight == 1))
        {
            searching = false;
            if (offsetTime <= 0)
            {
                selfSight.playerMissing = -1;
            }
            //"frustrated" sound effect
            if (Mathf.Abs(agent.remainingDistance - agent.stoppingDistance) <= 5)
            {
                returning = true;//run back to stalk point
            }
            else
            {
                selfSight.playerInSight = 1;
            }
        }

        anim.SetFloat("Speed_f", walk);
    }

    void Returning()
    {
        returning = true;
        stalking = false;
        chasing = false;
        searching = false;

        //print("destination is set");
        agent.destination = stalkObj.transform.position;
        //print(stalkObj.transform.position);
        //print("this is where he should be going: " + agent.destination);
        if (!agent.pathPending && (selfSight.playerInSight == 1 || agent.remainingDistance == agent.stoppingDistance))
        {
            //print("reached");
            returning = false;
        }
    }

    //stalking the player by standing at different locations based off of player position and staring at the player
    void Stalking()
    {
        stalking = true;
        chasing = false;
        searching = false;
        returning = false;
        agent.speed = 0f;
        offsetTime = 2f;
        //caught = false;

        //reset timer iff bad man attacked due to timer == 0
        if(timeAttack == true)
        {
            randTime = Random.Range(60.0f, 120.0f);
            timer = randTime;
            timeAttack = false;
        }

        anim.SetFloat("Speed_f", idle);

        Player.LightController.IsFlickering = false;

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
