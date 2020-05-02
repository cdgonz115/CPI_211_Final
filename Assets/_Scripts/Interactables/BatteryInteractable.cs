using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryInteractable : Interactable
{
    protected override void PerformAction()
    {
        base.PerformAction();

        AudioManager.singleton.PlayClip("Battery Sfx");

        Player.SetBatteryAmount(100);
    }
}
