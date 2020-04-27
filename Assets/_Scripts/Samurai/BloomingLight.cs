using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomingLight : MonoBehaviour
{
    public List<GameObject> lights;
    private float dimming;
    public int max;
    public int min;
    void Start()
    {
        
    }
    private void Update()
    {
        for (int x = 0; x < lights.Count; x++)
        {
            float temp= lights[x].GetComponent<Light>().intensity;
            if (temp <= min) dimming = 1;
            else if (temp >= max) dimming = -1;
            lights[x].GetComponent<Light>().intensity += (1f / 15f)*dimming;
        }
    }

}
