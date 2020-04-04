using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public GameObject LightObj;

    private void Update()
    {
        if (Input.GetButton("Fire1") && !LightObj.activeSelf && GameManager.singleton.BatteryAmount > 0)
        {
            LightObj.SetActive(true);

            GameManager.singleton.SetBatteryAmount();
        }
        else if(LightObj.activeSelf)
        {
            LightObj.SetActive(false);
        }
    }
}
