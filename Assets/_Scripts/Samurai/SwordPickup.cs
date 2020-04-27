using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : InventoryItem
{
    public List<GameObject> lights;
    private Color32 col = new Color32(255,0,0,255);
    protected override void PerformAction()
    {
        base.PerformAction();

        for (int x = 0; x < lights.Count; x++)
        {
            List<GameObject> temp = lights[x].GetComponent<BloomingLight>().lights;
            for (int y = 0; y < temp.Count; y++)
            {
                temp[y].GetComponent<Light>().intensity = 15;
                temp[y].GetComponent<Light>().color = col;
            }
            lights[x].GetComponent<BloomingLight>().enabled = false;
        }
    }
}
