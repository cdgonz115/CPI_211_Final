using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public string CurrentSceneName;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);

        if(string.IsNullOrEmpty(CurrentSceneName))
        {
            CurrentSceneName = SceneManager.GetActiveScene().name;
        }
    }

    /// <summary>
    /// Method that changes to a inputted scene and optionally saves
    /// the scene we are going into
    /// </summary>
    /// <param name="sceneName">The name of the scene to change to</param>
    /// <param name="saveScene">Set to true to save the scene. This is true by default</param>
    public void SetLevel(string sceneName, bool saveScene = true)
    {
        Scene nextScene = SceneManager.GetSceneByName(sceneName);

        if (nextScene != null)
        {
            if (saveScene)
            {
                CurrentSceneName = sceneName;
            }

            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// Method meant to be called by UI that will allow the player
    /// to retry a level. Will mostly be used in the game over scene
    /// </summary>
    public void RetryLevel()
    {
        if(CurrentSceneName != null)
        {
            SceneManager.LoadScene(CurrentSceneName);
        }
    }
}
