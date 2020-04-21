using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager singleton;

    public Text InteractionText;
    public FlashLightUI flashUI;
    public Text KeyUIText;

    public Transform InventoryParent;   //Parent reference used to spawn inventory item UI

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);

        InteractionText.gameObject.SetActive(false);
    }

    private void Update() 
    {
        flashUI.batteryValue = Player.BatteryAmount;

        if(KeyUIText != null)
        {
            int keyCount = Player.KeyCount;
            if(Player.HasLevelKey)
            {
                keyCount++;
            }
            KeyUIText.text = "Keys x " + keyCount;
        }
    }

    #region Interactable's

    public void ActivateInteractable(string description)
    {
        InteractionText.gameObject.SetActive(true);
        InteractionText.text = "Press 'E' to " + description;
    }

    public void DeactivateInteractable()
    {
        if(InteractionText != null)
        {
            InteractionText.gameObject.SetActive(false);
        }
    }

    #endregion
}
