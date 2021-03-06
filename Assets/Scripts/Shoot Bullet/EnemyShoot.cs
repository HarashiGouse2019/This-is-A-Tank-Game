﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform epointOfFire; //The transform component of the Enemy's point of fire
    public BulletMover ebulletPrefab; //The BulletMover component; We'll access the object associated with this component

    public Timer timer; //Reference our timer

    private void Update()
    {
        ebulletPrefab.transform.position = epointOfFire.position; //Update the position, and apply to the bullet
        ebulletPrefab.transform.rotation = epointOfFire.rotation; //Update the rotation, and apply to the bullet
        

        //Destroys if it doesn't have any more lives
        if (GameManager.instance.enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ShootOutObject(GameObject prefab)
    {
       prefab = ebulletPrefab.gameObject; //Assign the gameobject of the type BulletMover, and assign it to prefab
        Instantiate(prefab); //Create a gameobject
    }
}