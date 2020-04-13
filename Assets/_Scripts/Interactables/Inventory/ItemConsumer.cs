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

    protected override void PerformAction()
    {
        base.PerformAction();

        if(CollidingPlayer.HasItem(ItemToConsume.ItemName))
        {
            CollidingPlayer.RemoveInventory(ItemToConsume.ItemName);
        }
    }
}
