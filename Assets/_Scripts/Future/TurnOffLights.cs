using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLights : MonoBehaviour
{
    public GameObject turnBine1;
    public GameObject turnBine2;
    public GameObject dayLight;
    public List<GameObject> cityLights;
    public List<GameObject> otherLights;

    public GameObject turbineCam;
    public GameObject cityCam;
    public GameObject cinematicCamera;
    public GameObject playerCam;
    public GameObject drone;

    private WaitForSeconds ws = new WaitForSeconds(1 / 60);


    IEnumerator turnOff()
    {
        playerCam.SetActive(false);
        dayLight.SetActive(false);
        turbineCam.SetActive(true);
        for (int x = 0; x < 420; x++)
        {
            if (x % 10 == 0)
            {
                turnBine1.GetComponent<PowerRotator>().rotateBy *=.95f;
                turnBine2.GetComponent<PowerRotator>().rotateBy *=.95f;
            }
            yield return ws;
        }
        turbineCam.SetActive(false);
        cityCam.SetActive(true);
        for (int x = 0; x < cityLights.Count*40; x++)
        {
            if (x % 40 == 0) cityLights[x / 40].SetActive(false);
            yield return ws;
        }
        for (int x = 0; x < otherLights.Count; x++)
        {
            otherLights[x].SetActive(false);
        }
        cityCam.SetActive(false);
        cinematicCamera.SetActive(true);
        drone.GetComponent<DroneCrash>().enabled = true;
        for (int x=0; x < 4; x++)
        {
            yield return new WaitForSeconds(1);
        }
        cinematicCamera.SetActive(false);
        playerCam.SetActive(true);
        drone.GetComponent<DroneCrash>().enabled = false;
        gameObject.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") StartCoroutine("turnOff");
    }
}
