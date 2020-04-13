using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInteractable : Interactable
{
    public GameObject HideCamera;

    protected override void PerformAction()
    {
        base.PerformAction();

        if(Player.IsHiding)
        {
            Player.IsHiding = false;
            CollidingPlayer.PlayerCamera.SetActive(true);
            HideCamera.SetActive(false);
        }
        else
        {
            Player.IsHiding = true;
            CollidingPlayer.PlayerCamera.SetActive(false);
            HideCamera.SetActive(true);
        }
    }
}
