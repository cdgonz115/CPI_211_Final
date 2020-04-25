using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Interactable
{
    public GameObject door;
    public GameObject box;
    public List<GameObject> lights;


    override protected void PerformAction()
    {
        door.GetComponent<Animator>().enabled = true;
        box.GetComponent<LightFlickering>().enabled = false;
        for (int x = 0; x < lights.Count; x++)
        {
            lights[x].SetActive(false);
        }
    }
}

