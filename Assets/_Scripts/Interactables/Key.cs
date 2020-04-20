using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public GameObject SceneTransition;

    protected override void PerformAction()
    {
        base.PerformAction();

        SceneTransition.SetActive(true);
        Player.KeyCount++;
    }
}
