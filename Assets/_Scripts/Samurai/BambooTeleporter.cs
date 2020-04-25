using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooTeleporter : MonoBehaviour
{
    public Transform tpl;
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = tpl.position;
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
