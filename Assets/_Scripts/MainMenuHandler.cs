using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple class that handles the Main Menu scene.
/// Mostly the UI button press methods
/// </summary>
public class MainMenuHandler : MonoBehaviour
{
    public void PressPlay()
    {
        SceneManager.LoadScene("Museum");
    }

    public void PressQuit()
    {
        Application.Quit();
    }
}
