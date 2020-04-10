using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

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

        StartCoroutine(TestSceneTransition());
    }

    private void Start()
    {
        SetBatteryAmount(100);
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
