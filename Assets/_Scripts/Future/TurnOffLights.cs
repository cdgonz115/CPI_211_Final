using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLights : MonoBehaviour
{
    public GameObject turnBine1;
    public GameObject turnBine2;

    public GameObject cinematicCamera;
    public GameObject playerCam;
    public GameObject drone;


    IEnumerator turnOff()
    {
        turnBine1.GetComponent<PowerRotator>().enabled = false;
        turnBine2.GetComponent<PowerRotator>().enabled = false;
        playerCam.SetActive(false);
        cinematicCamera.SetActive(true);
        drone.GetComponent<DroneCrash>().enabled = true;
        for (int x=0; x < 4; x++)
        {
            yield return new WaitForSeconds(1);
        }
        cinematicCamera.SetActive(false);
        playerCam.SetActive(true);
        drone.GetComponent<DroneCrash>().enabled = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") StartCoroutine("turnOff");
    }
}
