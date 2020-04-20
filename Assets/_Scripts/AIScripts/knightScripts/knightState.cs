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
    }

    void Guarding()
    {
        chasing = false;
        guarding = true;
        agent.destination = postPoint.transform.position;
    }

}
