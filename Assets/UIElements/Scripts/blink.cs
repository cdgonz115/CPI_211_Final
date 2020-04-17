using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour
{
    public movingPlatform top;
    public movingPlatform bottom;

    
    private void Update() 
    {
        if(Input.GetMouseButtonDown(0) && !top.yMovement && !bottom.yMovement)
        {
            top.yMovement = true;
            bottom.yMovement = true;
        }

        if(top.oneRotation && bottom.oneRotation)
            {
                top.yMovement = false;
                bottom.yMovement = false;

                top.oneRotation = false;
                bottom.oneRotation = false;
            }
    }

    
    
}
