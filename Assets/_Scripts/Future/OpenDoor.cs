using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Interactable
{
    public GameObject door;
    public GameObject box;
    public List<GameObject> lights;

    public ShowDroneCinematic DroneCinematic;
    public float DoorOpenDelay; //Delay before the garage doors are opened
    public float LightOffDelay; //Delay before the lights are turned off

    override protected void PerformAction()
    {
        StartCoroutine(DelayDoorOpen());
    }

    /// <summary>
    /// Coroutine that opens the garage door, turns off the lights, then
    /// starts the show drone cinematic
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayDoorOpen()
    {
        CollidingPlayer.gameObject.SetActive(false);
        DroneCinematic.gameObject.SetActive(true);
        CanvasManager.singleton.DeactivateInteractable();

        //Wait then open the door
        yield return new WaitForSeconds(DoorOpenDelay);
        door.GetComponent<Animator>().enabled = true;
        box.GetComponent<LightFlickering>().enabled = false;

        //Waits then turns off the lights
        yield return new WaitForSeconds(LightOffDelay);
        for (int x = 0; x < lights.Count; x++)
        {
            lights[x].SetActive(false);
        }

        //Starts cinematic and gets rid of the garage opener
        DroneCinematic.StartCinematic();
        Destroy(gameObject);

        yield return null;
    }
}

