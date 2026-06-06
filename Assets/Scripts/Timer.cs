using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLimit = 60f;
    private float timeRemaining;
    private bool timerRunning = false;

    public void StartTimer()
    {
        timeRemaining = timeLimit;
        timerRunning = true;
        StartCoroutine(TimerCoroutine());
    }

    public void StopTimer()
    {
        timerRunning = false;
        StopAllCoroutines();
    }

    private IEnumerator TimerCoroutine()
    {
        while (timerRunning && timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }

        if (timeRemaining <= 0)
        {
            timerRunning = false;
            // Handle timer end logic here (e.g., notify other scripts, trigger events, etc.)
            Debug.Log("Timer has ended!");
        }
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}
