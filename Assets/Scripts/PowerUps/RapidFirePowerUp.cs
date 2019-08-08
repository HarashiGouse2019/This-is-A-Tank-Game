using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg = UnityEngine.Debug;

[System.Serializable]
public class RapidFirePowerUp : PowerUp
{
    public static RapidFirePowerUp instance;

    public float val, resetVal;

    public GameObject colSource;

    public override void OnApply(GameObject obj, GameObject source = null)
    {
        if (colSource != null) source = colSource;

        TankData temp = source.GetComponent<TankData>();
        resetVal = temp.rapidFireVal;
        temp.rapidFireVal = val;
        if (temp.shotsPerSecond > (temp.shotsPerSecond = temp.shotsPerSecond - (temp.rapidFireVal / 10)))
        {
            var tempVal = temp.rapidFireVal / 10;
            temp.shotsPerSecond = 1 - tempVal;
        }
        InputController.controller.canRapidFire = true;
    }

    public override void OnRemove(GameObject obj, GameObject source = null)
    {
        if (colSource != null) source = colSource;

        TankData temp = source.GetComponent<TankData>();
        temp.rapidFireVal = resetVal;
        temp.shotsPerSecond = temp.rapidFireVal;
        InputController.controller.canRapidFire = false;
    }
}
