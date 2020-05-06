using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    [Header("Class and Obj References")]
    private static FirstPersonController _fpsController;
    public GameObject PlayerCamera;
    public static FlashlightController LightController;
    
    [Header("Hiding")]
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
                PlayerMovement.FreezePlayer();
            }
            else
            {
                PlayerMovement.UnfreezePlayer();
                HidingObject = null;
            }
        }
    }
    public static GameObject HidingObject;

    [Header("Flashlight")]
    [SerializeField]
    private float _batteryDrainRate;
    public static float BatteryDrainRate;
    public static float BatteryAmount = -1;  //The default value of -1 indicates it needs to be set to 100 in Start()

    [Header("Misc")]
    public static PlayerMovement PlayerMovement;    //x = walk speed, y = run speed, z = jump speed
    public static int KeyCount = 0;
    public static bool HasLevelKey = false;
    private Dictionary<string, InventoryItem> _inventory;

    private void Awake()
    {
        BatteryDrainRate = _batteryDrainRate;

        _fpsController = GetComponent<FirstPersonController>();
        PlayerMovement = new PlayerMovement(_fpsController);

        _inventory = new Dictionary<string, InventoryItem>();

        LightController = GetComponent<FlashlightController>();

        IsHiding = false;
        HasLevelKey = false;
    }

    private void Start()
    {
        //-1 is the default value and the player hasn't been instantiated at all yet
        if (BatteryAmount == -1)
        {
            SetBatteryAmount(100);
        }
    }

    #region Battery

    public static void SetBatteryAmount(float newAmount)
    {
        BatteryAmount = newAmount;
    }

    #endregion

    #region Inventory

    public void AddInventory(InventoryItem item)
    {
        if (!_inventory.ContainsKey(item.ItemName))
        {
            GameObject itemUI = Instantiate(item.UIPrefab, CanvasManager.singleton.InventoryParent);
            item.SpawnedUI = itemUI;

            _inventory.Add(item.ItemName, item);
        }
        else
        {
            print(string.Format("Error Player.AddInventory({0}): Item already in inventory", item.ItemName));
        }
    }

    public void RemoveInventory(string itemName)
    {
        if(_inventory.ContainsKey(itemName))
        {
            Destroy(_inventory[itemName].SpawnedUI);

            _inventory.Remove(itemName);
        }
        else
        {
            print(string.Format("Error Player.RemoveInventory({0}): Item does not exist", itemName));
        }        
    }

    public bool HasItem(string itemName)
    {
        return _inventory.ContainsKey(itemName);
    }

    #endregion
}

/// <summary>
/// Struct used to handle all of the FPS Controller's movement
/// stuff. This allowed me to freeze and unfreeze the player
/// in one place
/// </summary>
public struct PlayerMovement
{
    private FirstPersonController _fpsController;

    public float WalkSpeed;
    public float RunSpeed;
    public float JumpSpeed;

    public PlayerMovement(FirstPersonController setController)
    {
        _fpsController = setController;

        WalkSpeed = _fpsController.WalkSpeed;
        RunSpeed = _fpsController.RunSpeed;
        JumpSpeed = _fpsController.JumpSpeed;
    }

    public void FreezePlayer()
    {
        _fpsController.WalkSpeed = 0;
        _fpsController.RunSpeed = 0;
        _fpsController.JumpSpeed = 0;
    }

    public void UnfreezePlayer()
    {
        _fpsController.WalkSpeed = WalkSpeed;
        _fpsController.RunSpeed = RunSpeed;
        _fpsController.JumpSpeed = JumpSpeed;
    }
}