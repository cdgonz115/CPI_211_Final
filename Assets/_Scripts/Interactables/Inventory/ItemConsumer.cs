using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is kind of a generic class that will be used to consume
/// a specific inventory item that the player has. This will likely be
/// inherited by another class and modified as needed
/// </summary>
public class ItemConsumer : Interactable
{
    public InventoryItem ItemToConsume;
    public string AudioCue;
    public GameObject iceBlock;

    protected new void Update()
    {
        if (CollidingPlayer != null && Input.GetKeyDown(KeyCode.E))
        {
            PerformAction();
        }
    }

    protected override void PerformAction()
    {
        base.PerformAction();

        if(CollidingPlayer.HasItem(ItemToConsume.ItemName))
        {
            if(!string.IsNullOrEmpty(AudioCue))
            {
                AudioManager.singleton.PlayClip(AudioCue);
            }

            CollidingPlayer.RemoveInventory(ItemToConsume.ItemName);
            StartCoroutine(melt());            
        }
    }
    IEnumerator melt()
    {
        WaitForSeconds ws = new WaitForSeconds(1f / 60f);
        for (int x = 0; x < 120; x++)
        {
            iceBlock.transform.localScale = new Vector3(5, iceBlock.transform.localScale.y * .95f, 2.5f);
            iceBlock.transform.localPosition = new Vector3(0, iceBlock.transform.localPosition.y - .01f, -1.09f);
            yield return ws;
        }
        if (DestroyOnUse)
        {
            Destroy(gameObject);
        }

    }

}
