using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg = UnityEngine.Debug;

public class Shoot : MonoBehaviour
{

    public Transform pointOfFire; //The transform of our point of fire
    public BulletMover bulletPrefab; //Reference the BulletMover component
    public TankData data;
    public GameObject litteExplosion;
    public bool canShoot = true;
 
    public Timer timer;
    public int waitVal = 1;

    private void Update()
    {
        bulletPrefab.gameObject.transform.position = pointOfFire.position; //Update the position, and apply it to our bullet
        bulletPrefab.gameObject.transform.rotation = pointOfFire.rotation;  //Update the rotation, and apply it to our bullet
        if (!canShoot) waitVal = Wait(data.shotsPerSecond);
    }
                                             
    public void ShootOutObject(GameObject prefab)
    {
        if (canShoot)
        {
            Instantiate(prefab); //Create our bullet prefap
            Instantiate(litteExplosion, pointOfFire.position, pointOfFire.rotation);
            AudioManager.manager.Play("FireSound");
            canShoot = false;
        }
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

    //Return a 1 if timer is up
    public int Wait(float seconds)
    {
        timer.StartTimer(3);
        UpdateCanShoot();
        if (timer.currentTime[3] > seconds)
        { 
            timer.ResetTime(3, false);
            return 1;
        }
        return 0;
    }

    public void UpdateCanShoot()
    {
        if (waitVal == 1) canShoot = true;
    }
}