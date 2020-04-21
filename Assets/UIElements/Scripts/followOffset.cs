using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followOffset : MonoBehaviour
{
     public GameObject eye;
     void Start () 
     {
        
     }
     
     void Update () 
     {
        this.transform.eulerAngles = eye.transform.eulerAngles;
     }
}
