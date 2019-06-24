using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //All the data that give us functionality to our tank
    public float moveSpeed; //Move speed
    public float reverseMoveSpeed;//Move speed of going backwards
    public float shotsPerSecond; //How many we can shoot
    public float rotateSpeed; //How fast we turn
    public float turretRotateSpeed; //How fast our turret turns
    public float health; //Our health
    public float maxHealth; //Our Max Health
    public float damageVal; //The amount of damage we can inflict
    public float shieldVal; //The amount/layer of shield you have
    public float rapidFireVal; //How fast you can shoot; this will use shotsPerSecond and modify it
    public Transform bodytf; //The transform of our body
    public Transform turrettf; //The transform of out turret
    public TankMover mover; //Our tank mover component
    public TurretMover turretMover; //Our turret mover component

    //Tank Data can apply to either the player or the computer
    public enum ControlMode
    {
        Player,
        Computer
    }
    public ControlMode mode; //Create an enum object called mode

    private void Awake()
    {
        if (bodytf == null || turrettf == null || turretMover == null || mover == null)
        {
            //Do Nothing
        }
        health = maxHealth;
    }
    private void OnTriggerStay(Collider collision)
    {
        switch (mode)
        {
            case ControlMode.Computer:       //If the enmey is hit with our bullet, have it take the player's damage
                if (collision.gameObject.name == "SD_Bullet(Clone)")
                {
                    health -= damageVal;
                }
                break;
            case ControlMode.Player:
                if (collision.gameObject.name == "EnemyBullet(Clone)")
                {
                    health -= damageVal;
                }
                break;
        }
    }
}

