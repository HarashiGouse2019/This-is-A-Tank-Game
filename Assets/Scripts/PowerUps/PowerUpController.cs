using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Temp
        if (powerups != null)
        {
            for (int i = 0; i < powerups.Count; i++)
            {
                timer.StartTimer(i);
                if (timer.currentTime[i] <= powerups[i].duration)
                {
                    toBeRemoved.Add(powerups[i]);
                    timer.ResetTime(i, false);
                }
            }

            //Once done iterated the power ups that need to be removed, remove them.
            for (int i = 0; i < toBeRemoved.Count; i++)
            {
                Remove(toBeRemoved[i]);
            }
        } else
        {
            Debug.Log("Kondaya, chi. Mo, chui dan ha iga pawaapu sseyou!");
        }
    }

    /// <summary>
    /// Appends a Power Up to an object.
    /// </summary>
    /// <param name="powerUp"></param>
    public void Append(PowerUp powerUp)
    {
        //Add powerUp to the list,
        powerups.Add(powerUp);

        //Call the OnApply event
        powerUp.OnApply(gameObject);
    }

    /// <summary>
    /// Removes an appended Power Up from an object.
    /// </summary>
    /// <param name="powerUp"></param>
    public void Remove(PowerUp powerUp)
    {
        //Remove powerup from the list,
        powerups.Remove(powerUp);

        //Call the OnRemove event
        powerUp.OnRemove(gameObject);
    }
}
