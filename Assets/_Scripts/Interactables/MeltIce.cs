using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltIce : ItemConsumer
{
    public GameObject iceBlock;
    public GameObject man;

    protected override void PerformAction()
    {
        if (CollidingPlayer.HasItem(ItemToConsume.ItemName))
        {
            if (!string.IsNullOrEmpty(AudioCue))
            {
                AudioManager.singleton.PlayClip(AudioCue);
            }
            man.GetComponent<moveTo>().chasing=true;
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
