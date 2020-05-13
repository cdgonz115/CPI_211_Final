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
    //private Rigidbody _rb;
    private GameObject playr;

    [Header("Eye Stuff")]
    public GameObject EyeLightObj;
    private Light _eyeLight;
    public float LightRate; //Rate that the eyes increase in intensity
    public float MaxLightLevel; //Intensity value till the player loses
    private Transform _playerCam;
    private float _initEyeLightLevel;

    [Header("Misc")]
    public bool _isCaught;
    public float StunDuration;
    public float MinBatteryAmount;
    private float killCooldown;
    private AudioSource[] aSources;
    private AudioSource ow;
    //private AudioSource fizzle;

    private void Awake()
    {
        _moveTo = GetComponent<moveTo>();
        //_rb = GetComponent<Rigidbody>();
        _eyeLight = EyeLightObj.GetComponent<Light>();

        _initEyeLightLevel = _eyeLight.intensity;
        playr = GameObject.FindGameObjectWithTag("Player");
        killCooldown = 0;
        AudioSource[] aSources = GetComponents<AudioSource>();
        ow = aSources[0];
        //fizzle = aSources[1];
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!Player.IsHiding && collision.gameObject.CompareTag("Player"))
    //    {
    //        //Freezes the player and bad man
    //        _rb.isKinematic = true;
    //        Player.PlayerMovement.FreezePlayer();
    //        _moveTo.suspended = true;

    //        EyeLightObj.SetActive(true);

    //        _isCaught = true;
    //        _playerCam = collision.gameObject.GetComponentInChildren<Transform>();
    //    }
    //}

    private void Update()
    {
        killCooldown -= Time.deltaTime;
        //on collision enter w/out needing rigidbody
        //---------------------------------------------------------------------------------------------------------------------------
        if(collidingY() && collidingX() && collidingZ() && killCooldown <= 0)//if colliding w/ player
        {
            //execute nick's onCollisionEnter(Collision collision) code
            if (_moveTo.chasing) //&& collision.gameObject.CompareTag("Player"))
            {
                //Freezes the player and bad man
                //_rb.isKinematic = true;
                Player.PlayerMovement.FreezePlayer();
                _moveTo.suspended = true;

                EyeLightObj.SetActive(true);

                _isCaught = true;

                killCooldown = 3;

                _playerCam = playr.gameObject.GetComponentInChildren<Transform>();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        if (_isCaught && _eyeLight.intensity <= MaxLightLevel)
        {
            _eyeLight.intensity += LightRate * Time.deltaTime;
            _eyeLight.range += LightRate * Time.deltaTime;

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
                        ow.Play();
                        //fizzle.Play();

                        StartCoroutine(Stun());
                    }
                }
            }
        }
        //Player loses
        else if(_isCaught)
        {
            GameManager.singleton.SetLevel("GONew", false);
        }
    }

    /// <summary>
    /// Coroutine used to stun the bad man. Also handles re-enabling
    /// the bad man after the stun
    /// </summary>
    public IEnumerator Stun()
    {
        GetComponent<AISight>().stunned = true;
        AudioManager.singleton.PlayClip("Supercharge Sfx");

        CanvasManager.singleton.Flash();
        EyeLightObj.SetActive(false);
        Player.PlayerMovement.ResetMovement();
        _isCaught = false;
        Player.SetBatteryAmount(0);

        yield return new WaitForSeconds(StunDuration);

        //_rb.isKinematic = false;
        _moveTo.suspended = false;
        GetComponent<AISight>().stunned = false;
        _moveTo.searching = true;
        _eyeLight.intensity = _initEyeLightLevel;

        yield return null;
    }

    private bool collidingY()
    {
        bool colY = false;

        if(Mathf.Abs(gameObject.transform.position.y - playr.transform.position.y) <= 2)
        {
            colY = true;
        }

        if(Player.IsHiding && _moveTo.chasing)
        {
            if (Mathf.Abs(gameObject.transform.position.y - playr.transform.position.y) <= 5)
            {
                colY = true;
            }
        }

        return colY;
    }

    private bool collidingX()
    {
        bool colX = false;

        if (Mathf.Abs(gameObject.transform.position.x - playr.transform.position.x) <= 2)
        {
            colX = true;
        }

        if (Player.IsHiding && _moveTo.chasing)
        {
            if (Mathf.Abs(gameObject.transform.position.x - playr.transform.position.x) <= 5)
            {
                colX = true;
            }
        }

        return colX;
    }

    private bool collidingZ()
    {
        bool colZ = false;

        if (Mathf.Abs(gameObject.transform.position.z - playr.transform.position.z) <= 2)
        {
            colZ = true;
        }

        if (Player.IsHiding && _moveTo.chasing)
        {
            if (Mathf.Abs(gameObject.transform.position.z - playr.transform.position.z) <= 5)
            {
                colZ = true;
            }
        }

        return colZ;
    }

}
