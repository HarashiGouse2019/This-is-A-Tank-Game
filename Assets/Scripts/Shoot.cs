using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform pointOfFire;
    public GameObject bulletPrefab;

    public Timer timer;
    
    private void Update()
    {
        bulletPrefab.transform.position = pointOfFire.position;
        bulletPrefab.transform.rotation = pointOfFire.rotation;
    }

    public void ShootOutObject(GameObject prefab)
    {
        Instantiate(prefab);
    }
}