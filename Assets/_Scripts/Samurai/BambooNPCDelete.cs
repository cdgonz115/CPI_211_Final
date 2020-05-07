using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooNPCDelete : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BambooNPC")
        {
            Destroy(other.gameObject);
            Destroy(this);
        }
    }
    
}
