using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueLight : MonoBehaviour
{
    public GameObject statue1;    
    public GameObject statue2;    
    public GameObject statue3;

    private void Update()
    {
        print(Vector3.Distance(transform.position, statue1.transform.position));
        closestStatue();
    }
    private void closestStatue()
    {
        float shortest = Vector3.Distance(transform.position,statue1.transform.position);
        float temp = Vector3.Distance(transform.position, statue2.transform.position);
        if (temp < shortest) shortest = temp;
        temp = Vector3.Distance(transform.position, statue3.transform.position);
        if (temp < shortest) shortest = Vector3.Distance(transform.position, statue3.transform.position);
    }
}

