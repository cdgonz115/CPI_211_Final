using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disguise : MonoBehaviour
{
    private int randomNum;
    private int numChildren;
    public bool ready;
    // Start is called before the first frame update
    void Start()
    {
        numChildren = gameObject.transform.GetChild(0).gameObject.transform.childCount;
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
       if (ready)
       {
            randomNum = (int)Random.Range(0.0f, numChildren);
            for(int i = 0; i < numChildren; i++)
            {
                if(gameObject.transform.GetChild(0).gameObject.transform.GetChild(i) == gameObject.transform.GetChild(0).gameObject.transform.GetChild(randomNum))
                {
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            ready = false;
        }
    }
}
