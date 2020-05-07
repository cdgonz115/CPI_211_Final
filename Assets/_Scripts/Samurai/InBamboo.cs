using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBamboo : MonoBehaviour
{
    public GameObject npc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            npc.GetComponent<NPCscriptedMove>().enabled = true;
            Destroy(this);
        }
    }
}
