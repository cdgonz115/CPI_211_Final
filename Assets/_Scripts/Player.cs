using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    private static FirstPersonController _fpsController;
    public GameObject PlayerCamera;

    public static Vector3 _playerSpeeds;    //x = walk speed, y = run speed, z = jump speed
    public static int KeyCount = 0;

    private static bool _isHiding;
    public static bool IsHiding
    {
        get
        {
            return _isHiding;
        }
        set
        {
            _isHiding = value;

            if (IsHiding)
            {
                _fpsController.WalkSpeed = 0;
                _fpsController.RunSpeed = 0;
                _fpsController.JumpSpeed = 0;
            }
            else
            {
                _fpsController.WalkSpeed = _playerSpeeds.x;
                _fpsController.RunSpeed = _playerSpeeds.y;
                _fpsController.JumpSpeed = _playerSpeeds.z;
            }
        }
    }

    [Header("Flashlight")]
    [SerializeField]
    private float _batteryDrainRate;
    public static float BatteryDrainRate;
    public static float BatteryAmount;

    private void Awake()
    {
        BatteryDrainRate = _batteryDrainRate;

        _fpsController = GetComponent<FirstPersonController>();
        _playerSpeeds = new Vector3(_fpsController.WalkSpeed, _fpsController.RunSpeed, _fpsController.JumpSpeed);
    }

    private void Start()
    {
        SetBatteryAmount(100);
    }

    #region Battery

    public static void SetBatteryAmount(float newAmount)
    {
        BatteryAmount = newAmount;
        CanvasManager.singleton.BatteryBar.fillAmount = BatteryAmount / 100;
    }

    public static void SetBatteryAmount()
    {
        SetBatteryAmount(BatteryAmount - BatteryDrainRate);
    }

    #endregion
}
