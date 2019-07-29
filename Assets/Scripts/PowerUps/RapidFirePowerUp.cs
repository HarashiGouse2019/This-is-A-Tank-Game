using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RapidFirePowerUp : PowerUp
{

    public float amplifier;

    public override void OnApply(GameObject obj)
    {
        Debug.Log("OnApply Called!");
        TankData temp = obj.GetComponent<TankData>();
        temp.rapidFireVal += amplifier;
    }

    public override void OnRemove(GameObject obj)
    {
        TankData temp = obj.GetComponent<TankData>();
        temp.rapidFireVal -= amplifier;
    }
}
