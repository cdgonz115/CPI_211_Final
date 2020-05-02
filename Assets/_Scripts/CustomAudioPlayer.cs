using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to literally play
/// an audio clip and destroy the obj once it's
/// done
/// </summary>
public class CustomAudioPlayer : MonoBehaviour
{
    public AudioSource Source;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        YieldInstruction delay = new WaitForEndOfFrame();

        Source.Play();

        while(Source.isPlaying)
        {
            yield return delay;
        }

        Destroy(gameObject);

        yield return null;
    }
}
