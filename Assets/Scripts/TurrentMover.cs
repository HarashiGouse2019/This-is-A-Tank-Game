using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentMover : MonoBehaviour
{
    public Transform tf;
    public TankData data;

    //For the camera
    public new Camera_Follow camera;
    public Vector3 center;
    public float angle = 0f;
    public float radius = 1f;


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
        //Camera movement
        center = tf.position;
        angle += data.turretRotateSpeed * Time.deltaTime;
        var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        tf.position = center + offset;

        //Actual turret movement
        data.turrettf.Rotate(new Vector3(0, direction * data.turretRotateSpeed * Time.deltaTime, 0));
    }
}