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
    public static float BatteryAmount = -1;  //The default value of -1 indicates it needs to be set to 100 in Start()

    [Header("Misc")]
    public static Vector3 _playerSpeeds;    //x = walk speed, y = run speed, z = jump speed
    public static int KeyCount = 0;
    public static bool HasLevelKey = false;
    private Dictionary<string, InventoryItem> _inventory;
    public float DeathDistance;

    private void Awake()
    {
        BatteryDrainRate = _batteryDrainRate;

        _fpsController = GetComponent<FirstPersonController>();
        _playerSpeeds = new Vector3(_fpsController.WalkSpeed, _fpsController.RunSpeed, _fpsController.JumpSpeed);

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

    /**
     * Notes: This determines if the player dies. I had to use the trigger
     * method and base it off a distance because of the way the bad man prefab is setup. The mix of colliders on the single
     * object made it so that OnCollisionEnter() never got called. This was my workaround
     */
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bad"))
        {
            float distance = Mathf.Abs(Vector3.Distance(other.transform.position, transform.position));

            if(distance <= DeathDistance)
            {
                GameManager.singleton.SetLevel("GameOver", false);
            }
        }
    }

    #region Battery

    public static void SetBatteryAmount(float newAmount)
    {
        BatteryAmount = newAmount;
    }

    public static void SetBatteryAmount()
    {
        SetBatteryAmount(BatteryAmount - BatteryDrainRate);
    }

    #endregion

    #region Inventory

    public void AddInventory(InventoryItem item)
    {
        if (!_inventory.ContainsKey(item.name))
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
