using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class EndingCinematic : MonoBehaviour
{
    public GameObject plyr;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject car;
    public GameObject man;
    public GameObject neck;
    public Image blackScreen;
    public GameObject ui;
    public GameObject BackgroundMusic;
    private bool started = false;

    IEnumerator cutScene()
    {
        WaitForSeconds ws = new WaitForSeconds(1f / 60f);
        plyr.GetComponent<FirstPersonController>().enabled = false;
        plyr.GetComponent<CharacterController>().enabled = false;
        plyr.GetComponentInChildren<Camera>().enabled = false;
        cam1.SetActive(true);
        ui.SetActive(false);
        BackgroundMusic.SetActive(false);
        for (int x = 0; x < 5 * 20; x++)
        {
            cam1.transform.position += new Vector3(0, 0, 1f/10f);
            yield return ws;
        }
        for (int x = 0; x < 5 * 8; x++)
        {
            cam1.transform.position += new Vector3(0, -1f/15f, 1f / 10f);
            yield return ws;
        }
        for (int x = 0; x < 5 * 10; x++)
        {
            cam1.transform.position += new Vector3(0,0, 1f / 10f);
            yield return ws;
        }
        cam1.SetActive(false);
        started = true;
        cam2.SetActive(true);
        AudioManager.singleton.PlayClip("Sport Sfx");
        for (int x = 0; x < 5 * 10; x++)
        {
            car.transform.position += new Vector3(x/100f, 0, 0);
            yield return ws;
        }
        for (int x = 0; x < 5 * 50; x++)
        {
            car.transform.position += new Vector3(.75f, 0, 0);
            cam2.transform.parent.position += new Vector3(0, 0, 1f / 30f);
            if (x == 5 * 45) man.SetActive(true);
            yield return ws;
        }
        for (int x = 0; x < 5 * 10; x++)
        {
            yield return ws;
        }
        for (int x = 0; x < 5 * 10; x++)
        {
            neck.transform.Rotate(new Vector3(-1, 0, 0), Space.Self);
            yield return ws;
        }
        for (int x = 0; x < 5 * 10; x++)
        {
            yield return ws;
        }

        for (int x = 0; x < 5 * 30; x++)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, x/150f);
            yield return ws;
        }
        Application.Quit();

    }

    private void Update()
    {
        if (started) cam2.transform.LookAt(car.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(cutScene());
        }
    }
}
