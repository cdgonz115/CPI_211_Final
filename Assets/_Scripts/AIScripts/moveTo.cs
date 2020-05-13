using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    private Transform playerPos;
    private GameObject[] player;
    public Transform lastPlayerSight;
    private float teleportCooldown=10;
    private float teleportTimer=0;
    private float timer;
    private bool timeAttack;
    private float randTime;
    private bool startOffset;

    //constants
    public float chaseSpeed = 8f;
    public float searchSpeed = 5f;
    private float offsetTime = 2f;
    private float distance=100;

    //animation states
    private float idle = 0.0f;
    private float run = 1.0f;
    private float walk = 0.5f;

    private AudioSource[] aSources;
    private AudioSource argh;
    private AudioSource chase;
    private AudioSource search;
    public AudioSource backgroundMusic;
    public float bmMaxVolume;
    public float bmMinVolume;

    //teleportation
    public Camera cam;
    public bool teleportEnabled = false;
    private RaycastHit hit;

    public Text message;
    private Animator anim;

    public GameObject stalkObj;

    private GameObject rightEye;
    private GameObject leftEye;

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
        //stalkObj = null;

        AudioSource[] aSources = GetComponents<AudioSource>();
        argh = aSources[1];
        chase = aSources[2];
        search = aSources[3];

        rightEye = GameObject.Find("glowEye");
        leftEye = GameObject.Find("glowEye (1)");
    }

    //state machine
    void FixedUpdate()
    {
       
        timer -= Time.deltaTime;
        teleportTimer -= Time.deltaTime;
        //rightEye.GetComponent<Material>().SetColor("_Color", Color.red * timer);
        //leftEye.GetComponent<Material>().SetColor("_Color", Color.red * timer);


        if (startOffset)
        {
            offsetTime -= Time.deltaTime;
        }

        if(suspended)
        {
            Suspended();//halt all manner of evilness
        }
        else if (selfSight.playerInSight == 1 || timer <= 0)//if the player is seen   // !Player.IsHiding && 
        {
            //manage sounds
            if(!chase.isPlaying)
            {
                backgroundMusic.volume = bmMinVolume;
                chase.Play();
            }
            if(search.isPlaying)
            {
                search.Stop();
            }

            //turn off eyes
            rightEye.GetComponentInChildren<Light>().range=.5f;
            leftEye.GetComponentInChildren<Light>().range=.5f;

            //set state
            DistanceToStalkpoints temp = player[0].GetComponent<DistanceToStalkpoints>();
            distance = Vector3.Distance(player[0].transform.position, transform.position);
            if (teleportEnabled && distance > 40 && temp.distance < distance && teleportTimer <= 0)
            {
                Vector3 check = cam.WorldToViewportPoint(temp.closest.transform.position);
                Vector3 onScreen = cam.WorldToViewportPoint(transform.position);
                //Check if bad man can be seen by the player
                if ((onScreen.x >= 0 && onScreen.x <= 1) && (onScreen.y >= 0 && onScreen.y <= 1) && (onScreen.z >= 0)) { }
                else
                {
                    //check if the closest stalking point can be seen by the player
                    if ((check.x >= 0 && check.x <= 1) && (check.y >= 0 && check.y <= 1) && (check.z >= 0))
                    {
                        if (Physics.Raycast(cam.transform.position, -(transform.position - temp.closest.transform.position), out hit, 35f))
                        {
                            //check if the player has a direct line of sight to the closest stalking point
                            if (hit.transform.tag != "StalkPoint" && hit.transform.tag != "boundary")
                            {
                                //print(hit.transform.name);
                                //print("first2 " + temp.distance);
                            }
                            else if (temp.sDistance < distance)
                            {
                                teleport(temp.sClosest);
                                //print("second2");
                            }
                        }
                    }
                    else
                    {
                        //check that the stalking point is at a minimum distance
                        if (temp.distance >= 5)
                        {
                            teleport(temp.closest);
                            //print("first1");
                        }
                        else if (temp.sDistance < distance)
                        {
                            teleport(temp.sClosest);
                            //print("second1");
                        }
                    }
                }
            }
            if (timer <= 0)
            {
                timeAttack = true;

                Chasing();//chase them
            }
            else
            {
                Chasing();
            }
               
             
        }
        else if ((Player.IsHiding && selfSight.playerMissing == 1) || searching)//if they went missing while they were being chased //|| selfSight.behindWall
        {
            //manage sounds
            if(chase.isPlaying)
            {
                chase.Stop();
            }
            if (!search.isPlaying)
            {
                search.Play();
            }

            //set state
            Searching();//search a bit
        }
        else if(returning)//&& selfSight.playerInSight == -1
        {
            //manage sounds
            if (chase.isPlaying)
            {
                chase.Stop();
            }
            if (search.isPlaying)
            {
                search.Stop();
            }

            //set state
            backgroundMusic.volume = bmMaxVolume/2f;
            Returning();
        }
        else//otherwise
        {
            //manage sounds
            if (chase.isPlaying)
            {
                chase.Stop();
            }
            if (search.isPlaying)
            {
                search.Stop();
            }
            backgroundMusic.volume = bmMaxVolume;
            //turn on eyes
            rightEye.GetComponentInChildren<Light>().range = .1f;
            leftEye.GetComponentInChildren<Light>().range = .1f;

            //set state
            Stalking();//keep stalking
        }
    }

    void teleport(GameObject closest)
    {
        transform.position = closest.transform.position;
        transform.forward = -(transform.position - player[0].transform.position);
        teleportTimer = teleportCooldown;
        agent.destination = playerPos.position;
    }
    void Suspended()
    {
        stalking = false;
        chasing = false;
        searching = false;

        agent.speed = 0;
        anim.SetFloat("Speed_f", idle);
    }

    //chasing the player by running at them according to the navmesh
    public void Chasing()
    {
        //AudioManager.singleton.PlayClip("(loop) Burning Pulse");
        //chase.Play();
        //reset timer iff bad man attacked due to timer == 0
        if (timeAttack == true)
        {
            randTime = Random.Range(60.0f, 120.0f);
            timer = randTime;
            timeAttack = false;
        }

        //if(!chasing)
        //{
        //    AudioManager.singleton.PlayClip("Chasing Sfx");
        //}

        stalking = false;
        chasing = true;
        searching = false;
        returning = false;
        agent.speed = chaseSpeed;

        agent.destination = playerPos.position;//chase the player
        
        anim.SetFloat("Speed_f", run);

        Player.LightController.IsFlickering = true;
    }

    //searching for the player since they "disappeared"
    void Searching()
    {
        //chase.Stop();
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
        
        if (!agent.pathPending && (Mathf.Abs(agent.remainingDistance - agent.stoppingDistance) <= 5 || selfSight.playerInSight == 1))
        {
            searching = false;
            if (offsetTime <= 0)
            {
                selfSight.playerMissing = -1;
            }
            //"frustrated" sound effect
            argh.Play();
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

        agent.destination = stalkObj.transform.position;
        anim.SetFloat("Speed_f", walk);

        if (!agent.pathPending && (selfSight.playerInSight == 1 || agent.remainingDistance == agent.stoppingDistance))
        {
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

    }
}
