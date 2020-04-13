using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointSight : MonoBehaviour
{
    private Transform detectedPosition;
    private GameObject[] detectedPlayer;
    private GameObject[] badMan;
    private moveTo agentDest;

    // Start is called before the first frame update
    void Start()
    {
        detectedPlayer = GameObject.FindGameObjectsWithTag("Player");//player reference
        detectedPosition = detectedPlayer[0].transform;
        badMan = GameObject.FindGameObjectsWithTag("Bad");
        agentDest = badMan[0].GetComponent<moveTo>();
    }

    // Update is called once per frame
    void Update()
    {
        detectedPosition = detectedPlayer[0].transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform == detectedPosition)
        {
            if (agentDest.stalking)
            {
                agentDest.agent.transform.position = this.transform.position;
            }
        }
    }
}
