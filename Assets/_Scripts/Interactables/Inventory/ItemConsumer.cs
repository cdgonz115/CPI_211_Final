using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is kind of a generic class that will be used to consume
/// a specific inventory item that the player has. This will likely be
/// inherited by another class and modified as needed
/// </summary>
public class ItemConsumer : Interactable
{
    public InventoryItem ItemToConsume;

    protected new void Update()
    {
        if (CollidingPlayer != null && Input.GetKeyDown(KeyCode.E))
        {
            PerformAction();
        }
    }

    protected override void PerformAction()
    {
        if(CollidingPlayer.HasItem(ItemToConsume.ItemName))
        {
            if(!string.IsNullOrEmpty(AudioCue))
            {
                AudioManager.singleton.PlayClip(AudioCue);
            }

            CollidingPlayer.RemoveInventory(ItemToConsume.ItemName); 

            if(DestroyOnUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
