using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public Battery[] batteries;
    public GameObject flash;
    public bool on;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(on)
        {
            flash.SetActive(true);
            GetComponent<Image>().color = new Color32(255,255,255,255);
            GetComponent<Outline>().effectColor = new Color32(255,255,255,255);
            for(int i = 0; i< batteries.Length; i++)
            {
                batteries[i].turnOn();
            }
        }

        else
        {
            flash.SetActive(false);
            GetComponent<Image>().color = new Color32(255,255,255,90);
            GetComponent<Outline>().effectColor = new Color32(255,255,255,90);
            for(int i = 0; i< batteries.Length; i++)
            {
                batteries[i].turnOff();
            }
        }
    }
}
