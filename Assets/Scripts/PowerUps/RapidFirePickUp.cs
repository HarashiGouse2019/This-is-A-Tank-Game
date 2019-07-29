using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickUp : MonoBehaviour
{
    public RapidFirePowerUp powerup;

    private void Update()
    {
        Debug.Log(powerup);
        
    }
    void OnTriggerEnter(Collider col)
    {
        PowerUpController tempPUC = col.GetComponent<PowerUpController>();
        if (tempPUC != null)
        {
            Debug.Log(col.gameObject);
            tempPUC.Append(powerup);
            
        }
    }
}
