using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningFuel: MonoBehaviour
{
    public Timer timer;
    public TankData player;

    [HideInInspector] public float fuel;

    private void Awake()
    {
        fuel = player.health;
    }
    private void Update()
    {
        BurnFuel();
    }
    public void BurnFuel()
    {
        const float sec = 0.25f;
        float fuelBurnedPerSec = 0.5f; //Fuel is between values 0 and 1;
        timer.StartTimer(0);
        if (timer.currentTime[0] > sec)
        {
            player.health -= fuelBurnedPerSec;
            GameManager.instance.healthUI.fillAmount = player.health / 100;
            fuel = player.health;
            timer.ResetTime(0, true);
        }
    }
}
