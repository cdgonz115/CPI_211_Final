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

    [Header("Flash")]
    public Image FlashImage;
    public float FlashBaseDuration;
    public float FlashClearDuration;
    public float FlashSpeed;
    public float FlashInitIntensity;

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

    public void ActivateInteractable(string description, bool useDescription = false)
    {
        InteractionText.gameObject.SetActive(true);
        if (!useDescription)
            InteractionText.text = "Press 'E' to " + description;
        else
            InteractionText.text = description;
    }

    public void DeactivateInteractable()
    {
        if(InteractionText != null)
        {
            InteractionText.gameObject.SetActive(false);
        }
    }

    #endregion

    /// <summary>
    /// Starts a cortoutine to flash the screen
    /// </summary>
    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    /// <summary>
    /// This flashes the player's screen as well as handles the flash clearing up.
    /// </summary>
    private IEnumerator FlashRoutine()
    {
        YieldInstruction clearDelay = new WaitForSeconds(FlashClearDuration);

        //Activates the white screen for a duration
        Color flashColor = FlashImage.color;
        flashColor.a = FlashInitIntensity;
        FlashImage.color = flashColor;
        yield return new WaitForSeconds(FlashBaseDuration);

        //While loop to clear the screen of the flash
        bool isClear = false;
        while(!isClear)
        {
            flashColor = FlashImage.color;
            flashColor.a -= FlashSpeed;

            if(flashColor.a <= 0)
            {
                isClear = true;
                flashColor.a = 0;
            }

            FlashImage.color = flashColor;

            yield return clearDelay;
        }

        yield return null;
    }
}
