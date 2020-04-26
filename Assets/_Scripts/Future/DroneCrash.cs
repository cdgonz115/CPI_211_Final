using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCrash : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject crashLocation;
    public float speed;

    public float MinDistance;

    public bool HasCrashed = false;

    public void OnEnable()
    {
        //crash();
    }
    void FixedUpdate()
    {
        float distance = Mathf.Abs(Vector3.Distance(transform.position, crashLocation.transform.position));
        if (distance > MinDistance) transform.position -= (transform.position - crashLocation.transform.position) * speed;
        else HasCrashed = true;
    }
}
