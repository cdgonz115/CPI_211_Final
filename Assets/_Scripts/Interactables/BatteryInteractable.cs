using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryInteractable : Interactable
{
    protected override void PerformAction()
    {
        base.PerformAction();

        Player.SetBatteryAmount(100);
    }
}
