using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Interactable
{
    public string NewSceneName;

    protected override void PerformAction()
    {
        base.PerformAction();

        if(NewSceneName.Equals("Museum") && Player.HasLevelKey)
        {
            Player.HasLevelKey = false;
            Player.KeyCount++;
        }

        GameManager.singleton.SetLevel(NewSceneName);
    }
}
