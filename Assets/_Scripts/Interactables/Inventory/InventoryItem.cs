using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class on items that can be added to the players inventory
public class InventoryItem : Interactable
{
    public string ItemName;

    public GameObject UIPrefab; //This is a prefab of UI that is displayed to show that the player picked up this item
    public GameObject SpawnedUI;

    private void Awake()
    {
        if(string.IsNullOrEmpty(ActionDescription))
        {
            ActionDescription = "Pickup " + ItemName;
        }
    }

    protected override void PerformAction()
    {
        base.PerformAction();

        CollidingPlayer.AddInventory(this);
    }
}
