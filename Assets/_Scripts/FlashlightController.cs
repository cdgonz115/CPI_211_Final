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

    [Header("Flicker")]
    public bool IsFlickering;
    public float FlickerRate;
    private float _flickerRateCount;
    public float FlickerDuration;
    private float _flickerDurationCount = 0;
    public float FlickerChance;

    private void Update()
    {
        if (Input.GetMouseButton(0) && Player.BatteryAmount > 0 && !Player.IsHiding)
        {
            IsOn = true;
        }
        else
        {
            IsOn = false;
        }

        if(IsOn && Player.BatteryAmount > 0 && !Player.IsHiding && _flickerDurationCount <= 0)
        {
            LightObj.SetActive(true);
            Player.SetBatteryAmount();

            if(IsFlickering)
            {
                _flickerRateCount -= Time.deltaTime;

                if(_flickerRateCount <= 0)
                {
                    float randInt = Random.value;

                    if(randInt < FlickerChance)
                    {
                        _flickerDurationCount = FlickerDuration;
                        LightObj.SetActive(false);
                    }

                    _flickerRateCount = FlickerRate;
                }
            }
        }
        else if(_flickerDurationCount > 0)
        {
            _flickerDurationCount -= Time.deltaTime;
        }
        else if(!IsOn && LightObj.activeSelf)
        {
            LightObj.SetActive(false);
        }
    }
}
