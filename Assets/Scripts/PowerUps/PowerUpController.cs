﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg = UnityEngine.Debug;

public class PowerUpController : MonoBehaviour
{
    public List<PowerUp> powerups;
    public Timer timer;

    // Update is called once per frame
    void Update()
    {
       HandlePowerUpTimers();
    }

    public void HandlePowerUpTimers()
    {
        List<PowerUp> toBeRemoved = new List<PowerUp>();
        for (int i = 0; i < powerups.Count; i++)
        {
            timer.StartTimer(i);
            if (timer.currentTime[i] > powerups[i].duration)
            {
                toBeRemoved.Add(powerups[i]);
                timer.ResetTime(i, false);
            }

        }

        //Once done iterated the power ups that need to be removed, remove them.
        if (toBeRemoved.Count > 0)
        {
            for (int i = 0; i < toBeRemoved.Count; i++)
            {
                Remove(toBeRemoved[i]);
            }
        }
    } 

    /// <summary>
    /// Appends a Power Up to an object.
    /// </summary>
    /// <param name="powerUp"></param>
    public void Append(PowerUp powerUp, GameObject source = null)
    {
        //Add powerUp to the list,
        powerups.Add(powerUp);

        //Call the OnApply event
        powerUp.OnApply(gameObject, source);

        //Pick Up Sound
        AudioManager.manager.Play("PowerUpPickUpSound");
    }

    /// <summary>
    /// Removes an appended Power Up from an object.
    /// </summary>
    /// <param name="powerUp"></param>
    public void Remove(PowerUp powerUp, GameObject source = null)
    {
        //Remove powerup from the list,
        powerups.Remove(powerUp);

        //Call the OnRemove event
        powerUp.OnRemove(gameObject, source);
    }
}
