using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject man;
    public float timerTwo = .5f;
    public float timer = 2;
    private bool stunned;
    private bool setoff;
    private void Start()
    {
        if (Player.KeyCount != 0) Destroy(this);
        man.SetActive(false);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        man.SetActive(true);
        setoff = true;

    }

    private void Update()
    {
        if (setoff) timerTwo -= Time.deltaTime;
        if (timerTwo <= 0) man.GetComponent<moveTo>().chasing=true;
        if (man.activeSelf)
        {
            if (man.GetComponent<CatchPlayer>()._isCaught)
            {
                CanvasManager.singleton.ActivateInteractable("Use flashlight to stun", true);
            }
            if (man.GetComponent<AISight>().stunned)
            {
                stunned = true;
                CanvasManager.singleton.ActivateInteractable("", true);
            }
        }
        if (stunned) timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(man);
            Destroy(this);
        }
    }
}
