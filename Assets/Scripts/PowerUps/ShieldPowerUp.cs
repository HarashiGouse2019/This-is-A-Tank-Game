using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    public static ShieldPowerUp instance;

    public float amplifier;

    public override void OnApply(GameObject obj)
    {
        Debug.Log("OnApply Called!");
        TankData temp = obj.GetComponent<TankData>();
        temp.shieldVal += amplifier;
    }

    public override void OnRemove(GameObject obj)
    {
        TankData temp = obj.GetComponent<TankData>();
        temp.shieldVal -= amplifier;
    }
}
