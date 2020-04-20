using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLights : Interactable
{
    public PowerRotator turnBine1;
    public PowerRotator turnBine2;
    public float minTurbineSpeed;
    public float turbineDecreaseSpeed;
    public GameObject lightO;
    public GameObject lightT;
    public GameObject dayLight;
    public List<GameObject> cityLights;
    public List<GameObject> otherLights;

    public GameObject turbineCam;
    public GameObject cityCam;
    public GameObject cinematicCamera;
    public GameObject playerCam;
    public DroneCrash drone;

    private bool _hasTurnedOff = false;

    [Header("Bad Man Spawn")]
    public GameObject StalkPointParent;
    public GameObject BadManPrefab;


    IEnumerator turnOff()
    {
        YieldInstruction frameDelay = new WaitForEndOfFrame();
        YieldInstruction secondDelay = new WaitForSeconds(.5f);

        playerCam.SetActive(false);
        dayLight.SetActive(false);
        turbineCam.SetActive(true);
        while(turnBine1.rotateBy > minTurbineSpeed || turnBine2.rotateBy > minTurbineSpeed)
        {
            print(turnBine1.rotateBy);

            turnBine1.rotateBy *= turbineDecreaseSpeed;
            turnBine2.rotateBy *= turbineDecreaseSpeed;

            yield return frameDelay;
        }
        turnBine1.enabled=false;
        turnBine2.enabled=false;
        lightO.SetActive(false);
        lightT.SetActive(false);
        turbineCam.SetActive(false);
        cityCam.SetActive(true);
        for (int x = 0; x < cityLights.Count; x++)
        {
            cityLights[x].SetActive(false);
            yield return secondDelay;
        }
        for (int x = 0; x < otherLights.Count; x++)
        {
            otherLights[x].SetActive(false);
        }
        cityCam.SetActive(false);
        cinematicCamera.SetActive(true);
        drone.enabled = true;
        while(!drone.HasCrashed)
        {
            yield return frameDelay;
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
