using FeTo.SOArchitecture;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] GameSO gameSettings;

    [Header("Events")]
    [SerializeField] FloatGameEvent updateTimeDisplay;
    [SerializeField] GameEvent timeOver;

    private float remainingTime;
    private void Start()
    {
        remainingTime = gameSettings.countdownInSeconds;
        StartCoroutine(GameOverCountDown());
    }

    private IEnumerator GameOverCountDown()
    {
        updateTimeDisplay.Raise(remainingTime);
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1);
            remainingTime -= 1;
            updateTimeDisplay.Raise(remainingTime);
        }
        timeOver.Raise();
    }
}
