using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnim : MonoBehaviour
{
    float randVal;
    float randTime;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        randVal = Random.Range(1.0f, 7.0f);
        randTime = Random.Range(3.0f, 6.0f);
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //    anim.SetBool("repeat", false);
        
        //print(randTime);
        randTime -= Time.deltaTime;
        if(randTime <= 0)
        {
            if(randVal == 6.0f)
                anim.SetInteger("Animation_int", 7);
            else
                anim.SetInteger("Animation_int", (int)randVal);

            if ((int)randVal == 0)
            {
                anim.SetBool("repeat", true);
                randTime = Random.Range(3.0f, 4.0f);
            }

            if ((int)randVal == 1)
            {
                anim.SetBool("repeat", true);
                randTime = Random.Range(4.0f, 7.0f);
            }

            if ((int)randVal == 2)
            {
                anim.SetBool("repeat", true);
                randTime = Random.Range(4.0f, 7.0f);
            }

            if ((int)randVal == 3)
            {
                anim.SetTrigger("watch");
                randTime = Random.Range(3.0f, 5.0f);
            }

            if ((int)randVal == 4)
            {
                anim.SetTrigger("dance");
                randTime = Random.Range(3.0f, 5.0f);
            }

            if ((int)randVal == 5)
            {
                anim.SetTrigger("smoke");
                randTime = Random.Range(3.0f, 5.0f);
            }

            if ((int)randVal == 6 || (int)randVal == 7)
            {
                anim.SetTrigger("wipe");
                randTime = Random.Range(3.0f, 5.0f);
            }

            //anim.SetTrigger("idleAction");
            
            randVal = Random.Range(0.0f, 7.0f);

        }
    }
}
