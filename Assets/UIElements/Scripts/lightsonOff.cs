using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightsonOff : MonoBehaviour
{
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject title;
    public float delayTime;
    private bool canShoot;
    private bool reloading;
    private int fullClip;
    public int clipSize;
    public int clipCount;
    public int reloadTime; 

    void Start()
    {
        canShoot = true;
        fullClip = clipSize;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(clipSize != 0)
        {
            if(canShoot)
            {

                StartCoroutine(shootingDelay(delayTime));

                clipSize--;
            } 
        }

        if(clipSize == 0 && clipCount!=0 && !reloading)
        {

            reloading = true;
            StartCoroutine(reloadDelay(reloadTime));
            clipCount--;
        }   
    }


    private IEnumerator shootingDelay(float delayLength)
    {
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        title.SetActive(false);
        canShoot = false;

        yield return new WaitForSeconds(delayLength);
        
        light1.SetActive(true);
        light2.SetActive(true);
        light3.SetActive(true);
        title.SetActive(true);
        canShoot = true;
        yield return null;
    }

    private IEnumerator reloadDelay(float delayLength)
    {
        yield return new WaitForSeconds(delayLength);
        light1.SetActive(true);
        light2.SetActive(true);
        light3.SetActive(true);
        title.SetActive(true);
        clipSize = fullClip;
        reloading = false;

    }

}
