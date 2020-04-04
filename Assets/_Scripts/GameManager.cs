using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public GameObject PlayerPrefab;

    [Header("Flashlight")]
    public float BatteryDrainRate;
    public float BatteryAmount;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        //StartCoroutine(TestSceneTransition());
    }

    private void Start()
    {
        SetBatteryAmount(100);
    }

    /// <summary>
    /// Method called when a new scene has been loaded. In this
    /// instance the player obj is spawned
    /// </summary>
    /// <param name="scene">Scene obj of the scene that was just loaded</param>
    /// <param name="mode">Mode the scene was just loaded in</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Transform playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").GetComponent<Transform>();

        Instantiate(PlayerPrefab, playerSpawn.position, playerSpawn.rotation);
    }

    public void SetBatteryAmount(float newAmount)
    {
        BatteryAmount = newAmount;
        CanvasManager.singleton.BatteryBar.fillAmount = BatteryAmount / 100;
    }

    public void SetBatteryAmount()
    {
        SetBatteryAmount(BatteryAmount - BatteryDrainRate);
    }

    /// <summary>
    /// This is a little coroutine that helped me test scene transition. 
    /// Delete it later
    /// </summary>
    /// <returns></returns>
    private IEnumerator TestSceneTransition()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("SceneTransitionTest");
    }
}
