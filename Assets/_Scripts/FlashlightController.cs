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

    private void Update()
    {
        if (Input.GetButton("Fire1") && !LightObj.activeSelf && Player.BatteryAmount > 0)
        {
            LightObj.SetActive(true);

            Player.SetBatteryAmount();
        }
        else if (LightObj.activeSelf)
        {
            LightObj.SetActive(false);
        }

    }
}
