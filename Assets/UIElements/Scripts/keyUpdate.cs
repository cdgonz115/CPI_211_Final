using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class keyUpdate : MonoBehaviour
{
    public Image keys;
    public Sprite nokey;
    public Sprite onekey;
    public Sprite twokey;
    public Sprite threekey;

    public int keyCount;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        switch(keyCount)
        {
            case 0:
                keys.sprite = nokey;
                break;
            case 1:
                keys.sprite = onekey;
                break;
            case 2:
                keys.sprite = twokey;
                break;
            case 3:
                keys.sprite = threekey;
                break;
        }

        if(Player.LightController.IsOn)
        {
            Color temp = keys.color;
            temp.a = 1;
            keys.color = temp;
        }

        else
        {
            Color temp = keys.color;
            temp.a = .4f;
            keys.color = temp;
        }
    }
}
