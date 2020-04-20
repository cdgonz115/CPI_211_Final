using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class knightState : MonoBehaviour
{
    //init vars
    public NavMeshAgent agent;
    
    private knightSight selfSight;

    private Transform playerPos;
    private GameObject[] player;
    private GameObject postPoint;

    public bool chasing;
    public bool guarding;

    public float speed = 8f;
    private float walkSpeed = 4f;

    //animation states
    private float idle = 0.0f;
    private float run = 1.0f;
    private float walk = 0.5f;

    private Animator anim;

    //public Vector3 postPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        postPoint = GameObject.Find("Post");
        selfSight = postPoint.GetComponent<knightSight>();
        player = GameObject.FindGameObjectsWithTag("Player");//player reference
        playerPos = player[0].transform;
        chasing = false;
        guarding = true;
        anim = gameObject.GetComponentInChildren<Animator>();
        //postPoint = postpointBoi.transform.position;
        //postPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (selfSight.playerInSight == 1 && postPoint.GetComponent<postBoundary>().inBounds == true)//if the player is seen
        {
            Chasing();
        }
        else
        {
            Guarding();
        }
    }

    void Chasing()
    {
        chasing = true;
        guarding = false;
        agent.speed = speed;
        agent.destination = playerPos.position;//chase the player
        anim.SetFloat("Speed_f", run);
    }

    void Guarding()
    {
        chasing = false;
        guarding = true;
        agent.speed = walkSpeed;
        print(agent.speed);
        agent.destination = postPoint.transform.position;

        if (agent.remainingDistance <= agent.stoppingDistance)//if we have arrived at a post, idle
        {
            gameObject.transform.rotation = postPoint.transform.rotation;
            anim.SetFloat("Speed_f", idle);

        }
        else//walk
        {
            anim.SetFloat("Speed_f", walk);

        }

    }

}
