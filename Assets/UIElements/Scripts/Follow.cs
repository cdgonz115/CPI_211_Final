using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
     
     public GameObject eye;
     
     void Start () 
     {

     }
     
     void Update () 
     {
         Vector3 pos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(20);
         Vector3 invertedMousePos = new Vector3(pos.x * (float).8, pos.y*(float).8, -pos.z);
         eye.transform.LookAt(invertedMousePos);
     }
 }
