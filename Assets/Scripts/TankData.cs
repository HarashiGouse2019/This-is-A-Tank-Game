using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public float moveSpeed;
    public float reverseMoveSpeed;
    public float shotsPerSecond;
    public float rotateSpeed;
    public float turretRotateSpeed;
    public Transform bodytf;
    public Transform turrettf;
    public TankMover mover;
    public TurrentMover turretMover;
}
