using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles when the player gets caught by the bad man.
/// When caught, player and bad man are essentially frozen. The bad mans
/// eyes start glowing really bright. At a certain light intensity the player
/// will be brought to the game over screen. If the player has the battery amount, they
/// are able to shine their light onto the bad man to stun him for a moment
/// </summary>
public class CatchPlayer : MonoBehaviour
{
    [Header("Class References")]
    private moveTo _moveTo;
    private Rigidbody _rb;

    [Header("Eye Stuff")]
    public GameObject EyeLightObj;
    private Light _eyeLight;
    public float LightRate; //Rate that the eyes increase in intensity
    public float MaxLightLevel; //Intensity value till the player loses
    private Transform _playerCam;
    private float _initEyeLightLevel;

    [Header("Misc")]
    private bool _isCaught;
    public float StunDuration;
    public float MinBatteryAmount;

    private void Awake()
    {
        _moveTo = GetComponent<moveTo>();
        _rb = GetComponent<Rigidbody>();
        _eyeLight = EyeLightObj.GetComponent<Light>();

        _initEyeLightLevel = _eyeLight.intensity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Player.IsHiding && collision.gameObject.CompareTag("Player"))
        {
            //Freezes the player and bad man
            _rb.isKinematic = true;
            Player.PlayerMovement.FreezePlayer();
            _moveTo.suspended = true;

            EyeLightObj.SetActive(true);

            _isCaught = true;
            _playerCam = collision.gameObject.GetComponentInChildren<Transform>();
        }
    }

    private void Update()
    {
        if(_isCaught && _eyeLight.intensity <= MaxLightLevel)
        {
            _eyeLight.intensity += LightRate;
            _eyeLight.range += LightRate;

            /**
             * Block of code that let's the player super charge their flashlight
             * if the shine it on the bad man and have the battery amount
             */
            if(Player.BatteryAmount >= MinBatteryAmount)
            {
                RaycastHit hit;
                if (Physics.Raycast(_playerCam.position, _playerCam.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject == gameObject && Player.LightController.IsOn)
                    {
                        StartCoroutine(Stun());
                    }
                }
            }
        }
        //Player loses
        else if(_isCaught)
        {
            GameManager.singleton.SetLevel("GameOver", false);
        }
    }

    /// <summary>
    /// Coroutine used to stun the bad man. Also handles re-enabling
    /// the bad man after the stun
    /// </summary>
    private IEnumerator Stun()
    {
        CanvasManager.singleton.Flash();
        EyeLightObj.SetActive(false);
        Player.PlayerMovement.UnfreezePlayer();
        _isCaught = false;
        Player.SetBatteryAmount(0);

        yield return new WaitForSeconds(StunDuration);

        _rb.isKinematic = false;
        _moveTo.suspended = false;
        _eyeLight.intensity = _initEyeLightLevel;

        yield return null;
    }
}
