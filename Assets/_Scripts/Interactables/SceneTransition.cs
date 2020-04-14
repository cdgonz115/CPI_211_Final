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

        GameManager.singleton.ChangeScene(NewSceneName);
    }
}
