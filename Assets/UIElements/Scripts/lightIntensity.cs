using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class lightIntensity : MonoBehaviour
{
    public Color full;
    public Color empty;
    public Image flash;


    // This will handle the brightness of the light UI 
    //gradient
    void Update()
    {
       flash.color = Color.Lerp(empty,full,Player.BatteryAmount/100);
    }
}
