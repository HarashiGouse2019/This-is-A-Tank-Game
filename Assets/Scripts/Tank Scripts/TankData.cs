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
    public float damage; //The amount of damage we can inflict
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
}
