using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRotator : MonoBehaviour
{
    public float rotateBy;
    
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+rotateBy,transform.eulerAngles.z);
    }
}
