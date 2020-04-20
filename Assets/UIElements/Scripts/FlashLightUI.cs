using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Class for the flashlight UI object that controls
//levels of batteries and chanes their look
//based on levels
public class FlashLightUI : MonoBehaviour
{
    public int currentBattery;
    public Battery[] batteries;
    public GameObject flashLight;
    public GameObject flash;
    public bool on;
    public float batteryValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && Player.BatteryAmount > 0)
        {
            on = true;
        }

        else if (!Input.GetMouseButton(0))
        {
            on = false;
        }

        //These if statements determine the current battery;
        if (batteryValue < 33) // Final battery
        {
            currentBattery = 0;
        }

        else if(batteryValue >= 33 && batteryValue <= 66 )//Second Battery
        {
            currentBattery = 1;
        }

        if(batteryValue > 66)//First Battery
        {
            currentBattery = 2;
        }

        //Switch statement that changes battery percentages
        //depending on the total battery level
        switch(currentBattery)
        {
            case 0:
                batteries[0].percent = batteryValue/33;
                batteries[1].percent = 0;
                batteries[2].percent = 0;
                break;
            case 1:
                batteries[0].percent = 1;
                batteries[1].percent = (batteryValue - 33)/33;
                batteries[2].percent = 0;
                break;
            case 2:
                batteries[0].percent = 1;
                batteries[1].percent = 1;
                batteries[2].percent = (batteryValue- 66)/33;
                break;
        }
        
        //If the flashlight is on, all elements are lit;
        if(on)
        {
            flash.SetActive(true);
            flashLight.GetComponent<Image>().color = new Color32(180,180,180,255);
            flashLight.GetComponent<Outline>().effectColor = new Color32(180,180,180,255);
            for(int i = 0; i< batteries.Length; i++)
            {
                batteries[i].turnOn();
            }

        }
        //If the elements are off, then elements are dimmed;
        else
        {
            flash.SetActive(false);
            flashLight.GetComponent<Image>().color = new Color32(180,180,180,90);
            flashLight.GetComponent<Outline>().effectColor = new Color32(180,180,180,90);
            for(int i = 0; i< batteries.Length; i++)
            {
                batteries[i].turnOff();
            }
        }
    }
}
