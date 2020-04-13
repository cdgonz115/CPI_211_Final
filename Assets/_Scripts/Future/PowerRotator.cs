using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRotator : MonoBehaviour
{
    public float rotateBy;
    
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+rotateBy,transform.eulerAngles.z);
    }
}
