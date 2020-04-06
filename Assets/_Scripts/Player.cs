using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int KeyCount = 0;

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

    public static void SetBatteryAmount(float newAmount)
    {
        BatteryAmount = newAmount;
        CanvasManager.singleton.BatteryBar.fillAmount = BatteryAmount / 100;
    }

    public static void SetBatteryAmount()
    {
        SetBatteryAmount(BatteryAmount - BatteryDrainRate);
    }
}
