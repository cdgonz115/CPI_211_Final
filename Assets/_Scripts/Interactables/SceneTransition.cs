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

        StartCoroutine(WaitChangeScene());
    }

    /// <summary>
    /// Coroutine that waits for the audio clip to play
    /// before transitiioning levels
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitChangeScene()
    {
        YieldInstruction delay = new WaitForEndOfFrame();

        GameObject sfx = AudioManager.singleton.PlayClip("Enter Level Sfx");

        while(sfx != null)
        {
            yield return delay;
        }

        GameManager.singleton.SetLevel(NewSceneName);
    }
}
