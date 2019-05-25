using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMover : MonoBehaviour
{
    public Transform tf;
    public TankData data;

    //For the camera
    public new Camera_Follow camera;
    public Vector3 center;
    public float angle = 0f;
    public float radius = 30f;


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        data = GetComponentInParent<TankData>();
        camera = camera.GetComponent<Camera_Follow>();
    }

    // Update is called once per frame
    public void Rotate(float direction)
    {
        //Actual turret movement
        data.turrettf.Rotate(new Vector3(0, direction * data.turretRotateSpeed * Time.deltaTime, 0));
        camera.Rotate(direction);
    }
}