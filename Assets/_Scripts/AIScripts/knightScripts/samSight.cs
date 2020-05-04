using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class samSight : MonoBehaviour
{
    public float view = 180f;//ai view in degrees
    public int playerInSight = -1;//state var
    public int playerMissing = -1;

    private SphereCollider coll;//"length" of eyesight
    public GameObject[] player;//player reference

    private GameObject knight;
    private samState stateRef;
    private samState selfState;//reference to state machine
    private postBoundary bounds;
    //private postBoundary boundsSc;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        coll = GetComponent<SphereCollider>();
        selfState = GetComponent<samState>();
        bounds = GetComponent<postBoundary>();
        knight = GameObject.Find("Samurai");
        stateRef = knight.GetComponent<samState>();

        //boundsSc = bounds.GetComponent<postBoundary>();
    }

    private void Update()
    {
        //if (selfState.searching) play = true;
    }
    void OnTriggerStay(Collider other)
    {
        bool hiding = Player.IsHiding;

        //check if the player is in sight
        if (GameObject.ReferenceEquals(other.gameObject, player[0]))
        {
            //--------------------------------------------------------------------------------------------------------------------------------------------
            playerInSight = -1;//default false
            //--------------------------------------------------------------------------------------------------------------------------------------------
            Vector3 directionPlayer = other.transform.position - transform.position; //make a vector pointing at the player
            float angle = Vector3.Angle(directionPlayer, transform.forward);//find angle between forward self and the player

            if (angle < 0.5f * view)//if player is less than half of our view angle...
            {
                RaycastHit wallChecker;

                if (Physics.Raycast(transform.position + transform.up / 2, directionPlayer.normalized, out wallChecker, coll.radius))//if there is a collider
                {
                    if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, player[0]) && stateRef.chasing && bounds.inBounds)//if it's the player and we're chasing them
                    {
                        playerInSight = 1;//we can see the player (there was no wall, so if they are hiding we saw them hide)
                        playerMissing = -1;
                    }
                    else if (GameObject.ReferenceEquals(wallChecker.collider.gameObject, player[0]) && stateRef.guarding)//if it's the player and we're hunting
                    {
                        playerInSight = 1;//can see player
                    }
                }
            }

        }

    }
}
