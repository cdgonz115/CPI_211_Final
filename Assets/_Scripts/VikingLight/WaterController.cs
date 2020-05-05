using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that removes the torch item from the player's
/// inventory when they collide
/// </summary>
public class WaterController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null && player.HasItem("Torch"))
            {
                player.RemoveInventory("Torch");
            }
        }
    }
}
