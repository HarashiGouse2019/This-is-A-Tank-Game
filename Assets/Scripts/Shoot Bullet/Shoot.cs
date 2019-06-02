using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform pointOfFire; //The transform of our point of fire
    public BulletMover bulletPrefab; //Reference the BulletMover component

    private void Update()
    {
  
        bulletPrefab.gameObject.transform.position = pointOfFire.position; //Update the position, and apply it to our bullet
        bulletPrefab.gameObject.transform.rotation = pointOfFire.rotation;  //Update the rotation, and apply it to our bullet
    }
                                             
    public void ShootOutObject(GameObject prefab)
    {
        Instantiate(prefab); //Create our bullet prefab
    }
}