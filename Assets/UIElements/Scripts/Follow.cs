using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
     
     public GameObject eye;
     public int xOffset;
     public int yOffset;
     private Vector3 offset;
     void Start () 
     {

     }
     
     void Update () 
     {
         offset =new Vector3(xOffset,yOffset,0);
         Vector3 pos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(20);
         Vector3 invertedMousePos = new Vector3(pos.x * (float).25, pos.y*(float).25, -pos.z);

         eye.transform.LookAt(invertedMousePos + offset);
     }
 }
