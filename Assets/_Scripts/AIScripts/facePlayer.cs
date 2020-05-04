using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facePlayer : MonoBehaviour
{
    private Transform playerPos;
    private GameObject[] player;
    private moveTo selfState;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        playerPos = player[0].transform;
        selfState = GetComponent<moveTo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos != null && selfState.stalking == true)
        {
            transform.LookAt(playerPos);
        }
    }
}
