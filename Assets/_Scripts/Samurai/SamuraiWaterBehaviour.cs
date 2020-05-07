using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiWaterBehaviour : MonoBehaviour
{
    public float speedModifier;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player.PlayerMovement.ModifySpeed(speedModifier);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player.PlayerMovement.ResetMovement();
        }
    }
}
