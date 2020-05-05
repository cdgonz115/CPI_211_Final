using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that removes the torch item from the player's
/// inventory when they collide
/// </summary>
public class WaterController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if(player != null && player.HasItem("Torch"))
            {
                player.RemoveInventory("Torch");
            }
        }
    }
}
