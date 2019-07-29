using System;
using System.Collections;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public static Camera_Follow camerafollow;

    public TankData data;

    //The Targeted GameObject to manipulate its position through it's Transform Component
    public GameObject target;

    //Setting the offset of the camera
    public Vector3 offset;

    private readonly Space offsetPositionSpace = Space.Self;

    private readonly bool lookAt = true;

    private void Start()
    {
        if (camerafollow == null)
        {
            camerafollow = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            ScanForPlayer();
        }
        Refresh();
    }

    public void Refresh()
    {
        

        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.transform.TransformPoint(offset);
        } else
        {
            transform.position = target.transform.position + offset;
        }

        //Compute Rotation
        if (lookAt)
        {
            transform.LookAt(target.transform);
        } else
        {
            transform.rotation = target.transform.rotation;
        }
    }

    public void ScanForPlayer()
    {
        target = FindObjectOfType<TurretMover>().gameObject;
        data = FindObjectOfType<TankData>(); //Grabs object with TankData script
    }
}

