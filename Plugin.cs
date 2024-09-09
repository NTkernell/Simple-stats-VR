using System.Collections;
using UnityEngine;
using BepInEx;

[BepInPlugin("com.ntkernel.PlayTimeroulette", "PlayTimeroulette", "1.0.0")]
public class timeroulette : BaseUnityPlugin
{
    private float roulette;

    void Start()
    {
        
        roulette = Random.Range(0f, 7200f);
        Logger.LogInfo($"Bye Bye in {roulette} seconds!");

        StartCoroutine(timetogo(roulette));
    }

    private IEnumerator timetogo(float delay)
    {
        yield return new WaitForSeconds(delay);

        Logger.LogInfo("Its time.");
        Application.Quit(); 
    }
}

