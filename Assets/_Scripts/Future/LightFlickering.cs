using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public GameObject light;
    public float timeInterval;
    public float timer;
    public bool inCR=false;
    WaitForSeconds ws = new WaitForSeconds(1/60);

    private void Update()
    {
        if(!inCR)timer += Time.deltaTime;
        if (!inCR && timer >= timeInterval) StartCoroutine(Flicker());
    }
    IEnumerator Flicker()
    {
        inCR = true;
        for (int x = 0; x < 40; x++)
        {
            if(x%5==0) light.SetActive(!light.activeSelf);
            yield return ws;
        }
        for (int x = 0; x < 40; x++) yield return ws;
        for (int x = 0; x < 40; x++)
        {
            if (x % 5 == 0) light.SetActive(!light.activeSelf);
            yield return ws;
        }
        for (int x = 0; x < 50; x++) yield return ws;
        for (int x = 0; x < 60; x++)
        {
            if (x % 5 == 0) light.SetActive(!light.activeSelf);
            yield return ws;
        }
        for (int x = 0; x < 20; x++) yield return ws;
        for (int x = 0; x < 40; x++)
        {
            if (x % 3 == 0) light.SetActive(!light.activeSelf);
            yield return ws;
        }
        for (int x = 0; x < 20; x++) yield return ws;
        for (int x = 0; x < 30; x++)
        {
            if (x % 2 == 0) light.SetActive(!light.activeSelf);
            yield return ws;
        }
        timer = 0;
        inCR = false;
    }


}
