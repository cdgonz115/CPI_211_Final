﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    protected override void PerformAction()
    {
        base.PerformAction();

        Player.KeyCount++;
    }
}
