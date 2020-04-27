using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class temp : MonoBehaviour
{
    public float view = 180f;//ai view in degrees
    public int playerInSight = -1;//state var
    public int playerMissing = -1;
    public AudioSource ads;
    private bool play = true;
    public bool behindWall;
    [SerializeField] private AudioClip[] sounds;

    //might use the following for hearing
    //>>
    //public Vector3 playerLastPosition;
    //private NavMeshAgent agent;        

    private SphereCollider coll;//"length" of eyesight
    public GameObject[] player;//player reference

    private moveTo selfState;//reference to state machine

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        coll = GetComponent<SphereCollider>();
        selfState = GetComponent<moveTo>();
    }

    private void Update()
    {
        if (selfState.searching) play = true;
        //make-shift onTriggerExit
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        if (Mathf.Abs(player[0].transform.position.x - transform.position.x) > coll.radius && Mathf.Abs(player[0].transform.position.z - transform.position.z) > coll.radius)
        {

            if (selfState.chasing)
            {
                //player is missing!
                playerMissing = 1;
                if (Player.IsHiding)
                {
                    playerInSight = -1;
                    selfState.agent.destination = Player.HidingObject.transform.position + Player.HidingObject.transform.forward;//go to where the player last was before they disappeared
                }
                else
                {
                    playerInSight = 1;
                }
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        bool hiding = Player.IsHiding;
        behindWall = false;

        //check if the player is in sight
        if (GameObject.ReferenceEquals(other.gameObject, player[0]))
        {
            //--------------------------------------------------------------------------------------------------------------------------------------------
            //playerInSight = -1;//default false
            //--------------------------------------------------------------------------------------------------------------------------------------------
            Vector3 directionPlayer = other.transform.position - transform.position; //make a vector pointing at the player
            float angle = Vector3.Angle(directionPlayer, transform.forward);//find angle between forward self and the player

            if (angle < 0.5f * view)//if player is less than half of our view angle...
            {
                RaycastHit wallChecker;

                if (Physics.Raycast(transform.position + transform.up / 2, directionPlayer.normalized, out wallChecker, coll.radius))//if there is a collider
                {
                    if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, player[0]) && selfState.chasing)//if it's the player and we're chasing them
                    {
                        playerInSight = 1;//we can see the player (there was no wall, so if they are hiding we saw them hide)

                    }
                    else if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, player[0]) && selfState.stalking)//if it's the player and we're hunting
                    {
                        if (hiding) //Player.IsHiding == true
                            playerInSight = -1;//cannot see player, player is hiding
                        else
                            //print("found you!");
                            playerInSight = 1;//can see player

                    }
                    else if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, player[0]) && selfState.searching)//if it's the player and we're searching
                    {
                        //print("there you are!");
                        if (hiding) //player[0].isHiding
                            playerInSight = -1;//cannot see player, player is hiding
                        else
                            playerInSight = 1;//can see player
                    }
                    else
                    {
                        behindWall = true;
                    }

                }
            }

        }
        
    }
}


