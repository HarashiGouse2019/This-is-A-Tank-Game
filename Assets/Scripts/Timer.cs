using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime = 0;
    public float resetTime;
    public bool timeStarted = false;

    private void Start()
    {
        resetTime = currentTime;
    }
    private void Update()
    {
        if (timeStarted)
        {
            currentTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        timeStarted = true;
    }
    public void ResetTime()
    {
        timeStarted = false;
        currentTime = resetTime;
    }
}
