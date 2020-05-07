using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToStalkpoints : MonoBehaviour
{
    public GameObject[] stalkpoints;
    public GameObject closest;
    private float distance=100;
    private int i = 0;

    private void Update()
    {
        distance = Vector3.Distance(stalkpoints[i].transform.position, transform.position);
        for (int x=0;x<stalkpoints.Length;x++)
        {
            float temp = Vector3.Distance(stalkpoints[x].transform.position, transform.position);
            
            if (distance > temp)
            {
                i = x;
                closest = stalkpoints[i];
            }
        }
       //print(stalkpoints[i].name);


    }
}
