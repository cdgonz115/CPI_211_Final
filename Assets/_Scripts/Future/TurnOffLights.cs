using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLights : Interactable
{
    public GameObject turnBine1;
    public GameObject turnBine2;
    public GameObject lightO;
    public GameObject lightT;
    public GameObject dayLight;
    public List<GameObject> cityLights;
    public List<GameObject> otherLights;

    public GameObject turbineCam;
    public GameObject cityCam;
    public GameObject cinematicCamera;
    public GameObject playerCam;
    public GameObject drone;

    private bool _hasTurnedOff = false;

    private WaitForSeconds ws = new WaitForSeconds(1f / 60f);

    [Header("Bad Man Spawn")]
    public GameObject StalkPointParent;
    public GameObject BadManPrefab;


    IEnumerator turnOff()
    {
        playerCam.SetActive(false);
        dayLight.SetActive(false);
        turbineCam.SetActive(true);
        for (int x = 0; x < 420; x++)
        {
            print("loop");
            if (x % 10 == 0)
            {
                turnBine1.GetComponent<PowerRotator>().rotateBy *=.95f;
                turnBine2.GetComponent<PowerRotator>().rotateBy *=.95f;
            }
            yield return ws;
        }
        turnBine1.GetComponent<PowerRotator>().enabled=false;
        turnBine2.GetComponent<PowerRotator>().enabled=false;
        lightO.SetActive(false);
        lightT.SetActive(false);
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

        //Spawns in bad man
        StalkPointParent.SetActive(true);
        Instantiate(BadManPrefab);
    }

    protected override void PerformAction()
    {
        base.PerformAction();

        if(!_hasTurnedOff)
        {
            StartCoroutine(turnOff());
            _hasTurnedOff = true;
            _displayUI = false;
            CanvasManager.singleton.DeactivateInteractable();
        }
    }
}
