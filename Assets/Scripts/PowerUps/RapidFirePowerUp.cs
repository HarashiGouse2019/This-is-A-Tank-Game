using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RapidFirePowerUp : PowerUp
{
    public float amplifier;

    public Timer timer;

    public override void OnActive()
    {
        if (timer.currentTime[0] > duration)
        {

        }
        throw new System.NotImplementedException();
    }
    public override void OnApply(GameObject obj)
    {
        TankData temp = obj.GetComponent<TankData>();
        temp.rapidFireVal += amplifier;
        timer.StartTimer(0);

        throw new System.NotImplementedException();
    }

    public override void OnRemove(GameObject obj)
    {
        TankData temp = obj.GetComponent<TankData>();
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter(Collider col)
    {
        PowerUpController tempPUC = col.GetComponent<PowerUpController>();
        if (tempPUC != null)
        {
            tempPUC.Append(null);
        }
    }
}
