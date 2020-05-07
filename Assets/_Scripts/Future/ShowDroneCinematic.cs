using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDroneCinematic : MonoBehaviour
{
    public Transform Drone;
    public GameObject Player;
    public Transform CamPivot;  //This pivot is used for the drone to stop then turn to the drone

    [Header("Movement")]
    public float MoveSpeed;
    private bool _movingToPivot;
    public float MinPivotDist;

    public float RotateSpeed;
    private bool _isRotating;

    public float MoveToDroneDistance;
    private bool _movingToDrone;
    private float _distMoved;

    [Header("Delays")]
    public float MoveDelay; //Delay before the drone exits the garage
    public float TurnDuration;  //Long the turning sequence lasts
    public float CloseLookDelay;    //Delay before the camera is shut off

    private void Update()
    {
        //Moves the camera to the pivot
        if(_movingToPivot)
        {
            transform.position += Vector3.Normalize(CamPivot.position - transform.position) * MoveSpeed * Time.deltaTime;

            if(Vector3.Distance(CamPivot.position, transform.position) <= MinPivotDist)
            {
                _movingToPivot = false;
                transform.position = CamPivot.position;
            }
        }

        /**
         * Rotates the camera towards the drone. Note that I couldn't think of any sort of exit
         * condition so uhh I just left it without one. Like dist of 2 obj can be calculated but idk how to calculate
         * if the camera is looking at the drone :shrug:
         */
        if (_isRotating)
        {
            Vector3 dirVector = Drone.transform.position - transform.position;
            float step = RotateSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirVector, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        //Moves the camera closer to the drone after it has rotated
        if(_movingToDrone)
        {
            Vector3 moveVector = Vector3.Normalize(Drone.position - transform.position) * MoveSpeed * Time.deltaTime;

            transform.position += moveVector;
            _distMoved += moveVector.magnitude;

            if (_distMoved >= MoveToDroneDistance)
            {
                _movingToDrone = false;
            }
        }
    }

    public void StartCinematic()
    {
        StartCoroutine(PlayCinematic());
    }

    private IEnumerator PlayCinematic()
    {
        YieldInstruction frameDelay = new WaitForEndOfFrame();

        //Waits then moves towards the pivot
        yield return new WaitForSeconds(MoveDelay);
        _movingToPivot = true;
        while(_movingToPivot)
        {
            yield return frameDelay;
        }

        //Starts rotating for a duration
        _isRotating = true;
        yield return new WaitForSeconds(TurnDuration);

        //Moves closer to the drone then waits
        _movingToDrone = true;
        while(_movingToDrone)
        {
            yield return frameDelay;
        }
        yield return new WaitForSeconds(CloseLookDelay);

        _movingToDrone = false;
        Player.SetActive(true);
        Destroy(gameObject);

        yield return null;
    }
}
