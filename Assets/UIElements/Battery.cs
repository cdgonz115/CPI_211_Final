using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    public Image fill;
    public float percent;
    public bool power;

    [Header("On Colors")]
    public Color fullBatt;
    public Color emptyBatt;
    
    [Header("Off Colors")]
    public Color offFull;
    public Color offEmpty;

    private void Start() 
    {

    }
    void Update()
    {
        if(power)
        {
            fill.color = Color.Lerp(emptyBatt,fullBatt,percent);
        }
        else
        {
            fill.color = Color.Lerp(offEmpty,offFull,percent);
        }
        
        fill.fillAmount = percent;
    }

    void setPercent(float fraction)
    {
        percent = fraction;
    }

    public void turnOff()
    {
        power = false;
        GetComponent<Image>().color = new Color32(255,255,255,90);
    }

    public void turnOn()
    {
        power = true;
        GetComponent<Image>().color = new Color32(255,255,255,255);
    }
}
