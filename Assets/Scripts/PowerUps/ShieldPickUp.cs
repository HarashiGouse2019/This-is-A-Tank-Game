using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    public ShieldPowerUp powerup;

    void OnTriggerEnter(Collider col)
    {
        PowerUpController tempPUC = col.GetComponent<PowerUpController>();
        if (tempPUC != null)
        {
            Debug.Log("Shield Power Up");
            tempPUC.Append(powerup, powerup.colSource = col.gameObject);
            Destroy(gameObject);
        }
    }
}
