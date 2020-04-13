using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCrash : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject crashLocation;

    public void OnEnable()
    {
        //crash();
    }
    void Update()
    {
        if(transform.position!=crashLocation.transform.position)transform.position+=(transform.position - crashLocation.transform.position)*-.02f;
    }
}
