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
        //Turns the flash light on and off
        if (Input.GetMouseButton(0) && Player.BatteryAmount > 0 && !Player.IsHiding)
        {
            IsOn = true;
        }
        else
        {
            //If it was just toggled off then play audio
            if(IsOn)
            {
                AudioManager.singleton.PlayClip("Flashlight Sfx");
            }

            IsOn = false;
        }

        //Plays audio for flash light
        if(Input.GetMouseButtonDown(0))
        {
            if(Player.BatteryAmount <= 0)
            {
                AudioManager.singleton.PlayClip("Battery Drained Sfx");
            }
            else if(IsOn)
            {
                AudioManager.singleton.PlayClip("Flashlight Sfx");
            }
        }

        //If else statements that control light flickering
        if(IsOn && Player.BatteryAmount > 0 && !Player.IsHiding && _flickerDurationCount <= 0)
        {
            LightObj.SetActive(true);
            Player.BatteryAmount -= Player.BatteryDrainRate * Time.deltaTime;

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
