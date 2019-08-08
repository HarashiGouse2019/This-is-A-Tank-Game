using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    public static ShieldPowerUp instance;

    public float amplifier;

    public GameObject colSource;

    public GameObject prefab;

    public override void OnApply(GameObject obj, GameObject source = null)
    {
        if (colSource != null) source = colSource;
        Instantiate(prefab, colSource.transform);
        Debug.Log("OnApply Called!");
        TankData temp = source.GetComponent<TankData>();
        temp.shieldVal += amplifier;
    }

    public override void OnRemove(GameObject obj, GameObject source = null)
    {
        if (colSource != null) source = colSource;

        TankData temp = source.GetComponent<TankData>();
        temp.shieldVal -= amplifier;
    }
}
