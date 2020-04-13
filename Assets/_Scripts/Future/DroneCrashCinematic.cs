using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCrashCinematic : MonoBehaviour
{
    public GameObject drone;
    void Update()
    {
        transform.LookAt(drone.transform);
    }
}
