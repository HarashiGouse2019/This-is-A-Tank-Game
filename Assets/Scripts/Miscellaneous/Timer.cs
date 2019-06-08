using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime = 0; //Current time
    public float resetTime; //The reset time
    public bool timeStarted = false; //If the time has started

    private void Start()
    {
        //Current time, when initialized, is set to 0. At the start of the game, assign the value of current time to reset time.
        resetTime = currentTime; 
    }
    
    private void Update()
    {
        if (timeStarted)
        {
            currentTime += Time.deltaTime; //current is increasing by Time.deltaTime.
        }
    }

    public void StartTimer()
    {
        //Starts the timer
        timeStarted = true;
    }
    public void ResetTime(bool continueTimer)
    {
        //Stops the timers, and returns the resetTime value back to the currentTime.
        switch(continueTimer)
        {
            case false:
                timeStarted = false;
                break;
            default:
                break;
        }
        currentTime = resetTime;
    }
}
