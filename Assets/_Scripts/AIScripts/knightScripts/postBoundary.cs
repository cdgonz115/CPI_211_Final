using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postBoundary : MonoBehaviour
{
    public bool inBounds;
    private GameObject guard;
    private SphereCollider coll;//"length" of eyesight
    //public GameObject[] player;//player reference

    private void Start()
    {
        inBounds = true;
        guard = GameObject.Find("Knight");
        //player = GameObject.FindGameObjectsWithTag("Player");
        coll = GetComponent<SphereCollider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == guard)
        {
            inBounds = false;
            //GetComponentInChildren<knightState>().guarding = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == guard)
        {
            inBounds = true;
        }

        //make-shift onTriggerExit
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        if (Mathf.Abs(guard.transform.position.x - transform.position.x) > coll.radius || Mathf.Abs(guard.transform.position.z - transform.position.z) > coll.radius)
        {
            //print("you've outrun me");
            //if (GameObject.ReferenceEquals(other.gameObject, guard))//if player is not in vision
            //{
                inBounds = false;
            //}
        }

    }
    
}
