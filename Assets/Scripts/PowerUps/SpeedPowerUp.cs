using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    public static SpeedPowerUp instance;

    public float amplifier;

    public GameObject colSource;

    public override void OnApply(GameObject obj, GameObject source = null)
    {
        if (colSource != null) source = colSource;

        Debug.Log("OnApply Called!");
        TankData temp = source.GetComponent<TankData>();
        temp.moveSpeed += amplifier;
        temp.reverseMoveSpeed += amplifier;
    }

    public override void OnRemove(GameObject obj, GameObject source = null)
    {
        if (colSource != null) source = colSource;

        TankData temp = source.GetComponent<TankData>();
        temp.moveSpeed -= amplifier;
        temp.reverseMoveSpeed -= amplifier;
    }
}
