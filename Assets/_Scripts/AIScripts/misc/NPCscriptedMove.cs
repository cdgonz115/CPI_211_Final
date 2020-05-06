using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCscriptedMove : MonoBehaviour
{
    //DESTINATIONS
    public Transform dest1;
    public Transform dest2;

    //NAVMESHAGENT
    private NavMeshAgent agent;

    //BOOLS
    public bool go = false;
    public bool firstOne = false;

    //ANIMATION REFERENCES
    private Animator anim;
    private NPCAnim idleSc;

    //TIMER
    public float timer = 5;
    public float timerReset = 5;

    // Start is called before the first frame update
    void Start()
    {
        //GET ANIMATORS
        agent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();

        //FIND DESTINATIONS
        //dest1 = GameObject.Find("NPCpoint").transform;
        //dest2 = GameObject.Find("NPCpoint (1)").transform;

        //IDLE SCRIPT REFERENCE
        idleSc = GetComponent<NPCAnim>();

        //MISC
        agent.speed = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when timer is 0, npc will go
        timer -= Time.deltaTime;
        if (timer <= 0)// && go == false
        {
            go = true;
            //timer = timerReset;
        }
        else
        {
            go = false;
        }

        if(go)//if the npc is going
        {
            idleSc.idle = false;
            anim.SetFloat("Speed_f", 0.5f);//walking anim

            if (timer <= 0 && firstOne)//if we're going to the first dest
            {
                agent.destination = dest1.position;//go there
                firstOne = false;//next time, we'll go to the other dest
            }
            else if(timer <= 0)//we're going to the other dest
            {
                agent.destination = dest2.position;//go there
                firstOne = true;//alternate dest
            }

            if(timer <= 0)
            {
                timer = timerReset;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)//if we've arrived at dest
            {
                go = false;//stop going
            }

            if(idleSc.idle == false)
            {
                anim.SetFloat("Speed_f", 0.5f);//walking anim
            }

        }
        else//we're not going
        {
            if (agent.remainingDistance <= agent.stoppingDistance)//if we've arrived at dest
            {
                anim.SetFloat("Speed_f", 0.0f);//idle anim
                idleSc.idle = true;
                //go = false;//stop going
            }
            else
            {
                anim.SetFloat("Speed_f", 0.5f);//walking anim
            }
            
        }
    }
}
