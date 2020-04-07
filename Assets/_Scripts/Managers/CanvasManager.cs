using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager singleton;

    public Text InteractionText;
    public Image BatteryBar;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);

        InteractionText.gameObject.SetActive(false);
    }

    private void Start()
    {
        BatteryBar.fillAmount = Player.BatteryAmount / 100;
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
