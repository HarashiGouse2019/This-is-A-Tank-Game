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
            Debug.Log("Rapid Fire Power Up");
            tempPUC.Append(powerup, powerup.colSource = col.gameObject);
            Destroy(gameObject);
        }
    }
}
