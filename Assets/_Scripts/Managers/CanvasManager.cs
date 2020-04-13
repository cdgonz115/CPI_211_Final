using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager singleton;

    public Text InteractionText;
    public FlashLightUI flashUI;

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
    }

    #region Interactable's

    public void ActivateInteractable(string description)
    {
        InteractionText.gameObject.SetActive(true);
        InteractionText.text = "Press 'E' to " + description;
    }

    public void DeactivateInteractable()
    {
        InteractionText.gameObject.SetActive(false);
    }

    #endregion
}
