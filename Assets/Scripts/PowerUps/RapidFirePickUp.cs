using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickUp : MonoBehaviour
{
    public RapidFirePowerUp powerup;

    void OnTriggerEnter(Collider col)
    {
        PowerUpController tempPUC = col.GetComponent<PowerUpController>();
        if (tempPUC != null)
        {
            tempPUC.Append(powerup, powerup.colSource = col.gameObject);
            Destroy(gameObject);
        }
    }
}
