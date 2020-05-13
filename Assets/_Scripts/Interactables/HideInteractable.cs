using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInteractable : Interactable
{
    public float timer;
    public float timerReset = .5f;
    public GameObject HideCamera;
    public bool hiding;
    protected override void PerformAction()
    {
        base.PerformAction();

        if(Player.IsHiding)
        {
            hiding = false;
            Player.IsHiding = false;
            CollidingPlayer.transform.gameObject.GetComponent<CharacterController>().enabled = true;
            CollidingPlayer.PlayerCamera.SetActive(true);
            HideCamera.SetActive(false);
        }
        else
        {
            hiding = true;
            Player.IsHiding = true;
            CollidingPlayer.PlayerCamera.SetActive(false);
            CollidingPlayer.transform.gameObject.GetComponent<CharacterController>().enabled = false;
            HideCamera.SetActive(true);
            Player.HidingObject = gameObject;
            timer = timerReset;
        }
    }
    private void FixedUpdate()
    {
        if(hiding)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && GameObject.FindGameObjectWithTag("Bad").GetComponent<AISight>().playerInSight == 1) CanvasManager.singleton.ActivateInteractable("He saw you hide, run", true);
            else CanvasManager.singleton.ActivateInteractable("", true);
        }
        
    }
}
