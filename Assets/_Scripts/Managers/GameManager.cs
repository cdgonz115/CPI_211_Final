using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public Scene CurrentScene;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);

        if(CurrentScene == null)
        {
            CurrentScene = SceneManager.GetActiveScene();
        }
    }

    public void ChangeScene(string sceneName)
    {
        Scene nextScene = SceneManager.GetSceneByName(sceneName);

        if (nextScene != null)
        {
            CurrentScene = nextScene;

            SceneManager.LoadScene(sceneName);
        }
    }
}
