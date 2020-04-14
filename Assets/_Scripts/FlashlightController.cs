using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that handles the flashlight
/// that the player uses
/// </summary>
public class FlashlightController : MonoBehaviour
{
    public GameObject LightObj;
    public FlashLightUI flashObj;
    public bool IsOn;

    private void Awake()
    {
        //Turns off flashlight on default
        IsOn = false;
        LightObj.SetActive(false);
    }

    private void Update()
    {
        //Toggles the flashlight
        if(Input.GetButtonDown("Fire1"))
        {
            IsOn = !IsOn;

            if(!IsOn)
            {
                LightObj.SetActive(false);
            }
            else
            {
                LightObj.SetActive(true);
            }
        }

        //Drains the battery
        if (IsOn)
        {
            Player.SetBatteryAmount();

            //Disables flashlight if it ran out of battery
            if(Player.BatteryAmount <= 0)
            {
                IsOn = false;
                LightObj.SetActive(false);
            }
        }
    }
}
