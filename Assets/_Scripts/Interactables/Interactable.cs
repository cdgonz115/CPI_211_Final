using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class of all objects that can be interacted with. As the parent,
/// all interactables will pop up UI to press E to do whatever the action is.
/// It will then call a method for that action. All children need to override
/// this action to give it functionality.
/// </summary>
public class Interactable : MonoBehaviour
{
    public string ActionDescription;
    public bool DestroyOnUse;

    protected bool _displayUI = true;

    public Player CollidingPlayer;

    protected void OnDestroy()
    {
        CanvasManager.singleton.DeactivateInteractable();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _displayUI)
        {
            CanvasManager.singleton.ActivateInteractable(ActionDescription);

            CollidingPlayer = other.GetComponent<Player>();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasManager.singleton.DeactivateInteractable();

            CollidingPlayer = null;
        }
    }

    protected void Update()
    {
        if (CollidingPlayer != null && Input.GetKeyDown(KeyCode.E))
        {
            PerformAction();

            if (DestroyOnUse)
                Destroy(gameObject);
        }
    }

    protected virtual void PerformAction() { }
}
