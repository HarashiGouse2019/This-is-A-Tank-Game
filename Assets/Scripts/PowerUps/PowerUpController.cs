using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public List<PowerUp> powerups; //Temp

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandlePowerUpTimers();   
    }

    public void HandlePowerUpTimers()
    {
        List<PowerUp> toBeRemoved = new List<PowerUp>();
        Timer timer = new Timer();
        //Temp
        for (int i = 0; i < powerups.Count; i++)
        {
            timer.StartTimer(0);
            if (timer.currentTime[0] <= powerups[i].duration)
            {
                toBeRemoved.Add(powerups[i]);
                timer.ResetTime(0, false);
            }
        }

        //Once done iterated the power ups that need to be removed, remove them.
        for (int i = 0; i < toBeRemoved.Count; i++)
        {
            Remove(toBeRemoved[i]);
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
