using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform pointOfFire; //The transform of our point of fire
    public BulletMover bulletPrefab; //Reference the BulletMover component
    public Timer timer;

    private void Update()
    {
  
        bulletPrefab.gameObject.transform.position = pointOfFire.position; //Update the position, and apply it to our bullet
        bulletPrefab.gameObject.transform.rotation = pointOfFire.rotation;  //Update the rotation, and apply it to our bullet
    }
                                             
    public void ShootOutObject(GameObject prefab)
    {
        Instantiate(prefab); //Create our bullet prefab
    }

    public void InitiateEnemyControls(float secondsUntilShoot)
    {
        timer.StartTimer(2); //Start our timer
        if (timer.currentTime[2] > secondsUntilShoot) //If timer's current time is greater than our set duration
        {
            ShootOutObject(bulletPrefab.gameObject); //Shoot a bullet
            timer.ResetTime(2, false); //Reset the timer
        }
    }
}