using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the game over scene
/// by assigning button actions and whatnot
/// </summary>
public class GameOverSceneHandler : MonoBehaviour
{
    public Button RetryButton;
    public Button QuitButton;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        RetryButton.onClick.AddListener(delegate 
        { 
            GameManager.singleton.RetryLevel();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        });
        QuitButton.onClick.AddListener(delegate { Application.Quit(); });
    }
}
