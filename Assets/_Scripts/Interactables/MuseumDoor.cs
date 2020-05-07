using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumDoor : Interactable
{
    protected new void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _displayUI)
        {
            CanvasManager.singleton.ActivateInteractable(ActionDescription, true);

            CollidingPlayer = other.GetComponent<Player>();
        }
    }

    protected new void Update()
    {
        base.Update();

        if(Player.KeyCount == 3)
        {
            ActionDescription = "Escape";
        }
        else
        {
            ActionDescription = "Needs " + (3 - Player.KeyCount) + " keys to unlock door";
        }
    }

    protected override void PerformAction()
    {
        base.PerformAction();

        if(Player.KeyCount == 3)
        {
            print("Win");
        }
    }
}
