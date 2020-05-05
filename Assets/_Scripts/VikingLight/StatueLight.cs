﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueLight : MonoBehaviour
{
    public GameObject[] statues;
    public GameObject lightPrefab;
    private GameObject SLight;
    private int closest;

    private void Start()
    {
        SLight = Instantiate(lightPrefab, transform.position, transform.rotation);
    }
    private void Update()
    {
        float dis=closestStatue();
        SLight.transform.position = transform.position - (transform.position - statues[closest].transform.position) * .007f;
        if(dis <= 500f) SLight.GetComponent<Light>().intensity= dis / 500f;
    }
    private float closestStatue()
    {
        float shortest = Vector3.Distance(transform.position, statues[0].transform.position);
        float temp = Vector3.Distance(transform.position, statues[1].transform.position);
        closest = 0;
        if (temp < shortest)
        {
            shortest = temp;
            closest = 1;
        }
        temp = Vector3.Distance(transform.position, statues[2].transform.position);
        if (temp < shortest)
        {
            shortest = temp;
            closest = 2;
        }
        return shortest;
    }
}

