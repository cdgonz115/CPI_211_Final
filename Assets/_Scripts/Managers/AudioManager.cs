using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to handle audio. The way the system
/// works is that when an audio is to be played it
/// spawns a gameobject that has the desired audio clip. Once
/// that clip finishes then the object with the audio source is destroyed
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager singleton;

    private GameObject[] _audioClips;

    //Note: RSN stands for random scary noise
    [Header("Random Scary Noise")]
    public bool PlayRSN;
    public float minTimeRSN;
    public float maxTimeRSN;
    private float _countRSN;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);

        //Loads and saves all audio clip obj's from the resources folder
        Object[] loadedClips = Resources.LoadAll("AudioClips");
        _audioClips = new GameObject[loadedClips.Length];
        for(int x = 0; x < _audioClips.Length; x++)
        {
            _audioClips[x] = (GameObject) loadedClips[x];
        }
    }

    private void Start()
    {
        _countRSN = Random.Range(minTimeRSN, maxTimeRSN);
    }

    private void Update()
    {
        if(PlayRSN)
        {
            if (_countRSN <= 0)
            {
                PlayClip("Scary Noise");
                _countRSN = Random.Range(minTimeRSN, maxTimeRSN);
            }
            else
            {
                _countRSN -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Plays a desired audio clip by spawning a clip obj
    /// </summary>
    /// <param name="clipName">The name of the clip to play</param>
    /// <param name="newParent">An optional transform to use as the clips parent</param>
    /// <returns>Returns the generated audio clip obj</returns>
    public GameObject PlayClip(string clipName, Transform newParent = null)
    {
        GameObject clipToPlay = null;

        foreach(GameObject clip in _audioClips)
        {
            if(clip.name.Equals(clipName))
            {
                clipToPlay = clip;
            }
        }

        if(clipToPlay == null)
        {
            print("Error AudioManager.PlayClip: Clip " + clipName + " now found");
            return null;
        }

        CustomAudioPlayer player = null;
        if(newParent == null)
        {
            player = Instantiate(clipToPlay).GetComponent<CustomAudioPlayer>();
        }
        else
        {
            player = Instantiate(clipToPlay, newParent).GetComponent<CustomAudioPlayer>();
        }

        player.PlayClip();

        return player.gameObject;
    }
}
