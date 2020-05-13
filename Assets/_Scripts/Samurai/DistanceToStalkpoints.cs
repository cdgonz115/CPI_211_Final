using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToStalkpoints : MonoBehaviour
{
    public Camera cam;
    public GameObject[] stalkpoints;
    public GameObject closest;
    public GameObject sClosest;
    public float sDistance;
    public float distance=100;
    private int i = 0;
    private int ii = 0;
    private RaycastHit hit;

    private void Update()
    {
        distance=Vector3.Distance(stalkpoints[i].transform.position, transform.position);
        sDistance=Vector3.Distance(stalkpoints[ii].transform.position, transform.position);
        for (int x=0;x<stalkpoints.Length;x++)
        {
            float temp = Vector3.Distance(stalkpoints[x].transform.position, transform.position);
            
            //Check if the new stalkpoint is closer than the current one
            if (distance > temp)
            {
                if(closest!= stalkpoints[x]) changeClosest(x);        
            }
            
        }        
        void changeClosest(int x)
        {
            ii = i;
            i = x;
            sClosest = closest;
            closest = stalkpoints[i];
        }
    }
}
