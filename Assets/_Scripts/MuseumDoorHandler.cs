using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles opening and closing doors based on how many
/// keys the player has
/// </summary>
public class MuseumDoorHandler : MonoBehaviour
{
    public GameObject[] ClosedDoors;
    public GameObject[] OpenDoors;

    private void Start()
    {
        if (ClosedDoors.Length != OpenDoors.Length)
        {
            print("WARNING: MuseumDoorHandler.ClosedDoors and MuseumDoorHandler.OpenDoors may not be setup correctly");
        }
        else
        {
            SetupDoors();
        }
    }

    private void SetupDoors()
    {
        for(int x = 0; x < ClosedDoors.Length; x++)
        {
            if(x <= Player.KeyCount)
            {
                ClosedDoors[x].SetActive(false);
                OpenDoors[x].SetActive(true);
            }
            else
            {
                ClosedDoors[x].SetActive(true);
                OpenDoors[x].SetActive(false);
            }
        }
    }
}
