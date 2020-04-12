using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PlayerCamera;

    public static int KeyCount = 0;
    public static bool IsHiding = false;

    [Header("Flashlight")]
    [SerializeField]
    private float _batteryDrainRate;
    public static float BatteryDrainRate;
    public static float BatteryAmount;

    private void Awake()
    {
        BatteryDrainRate = _batteryDrainRate;
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
