using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facePlayer : MonoBehaviour
{
    private Transform playerPos;
    private GameObject[] player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        playerPos = player[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos != null)
        {
            transform.LookAt(playerPos);
        }
    }
}
