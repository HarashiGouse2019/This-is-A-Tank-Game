using System;
using System.Collections;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public static Camera_Follow camerafollow;

    public TankData data;

    //The Targeted GameObject to manipulate its position through it's Transform Component
    public GameObject target;

    //Used to set the duration of the camera smoothing out and in towards the player
    public float smoothOutDuration = 0.125f;

    public float camRotationSpeed; // How fast the camera rotates around the player

    //Setting the offset of the camera
    public Vector3 offset;

    Vector3 setCoordinate; //Set up camera positioning
    Vector3 smoothPosition; //This position is use to create a smooth effect for our camera

    private Space offsetPositionSpace = Space.Self;

    private bool lookAt = true;

    void Start()
    {
        //FindObjectOfType interates through all of the known gameobjects, and returns the first one with a defined component.
        //That is why you should use it in the start or awake functions
        target = FindObjectOfType<TurretMover>().gameObject; //Grabs the object with TurretMover script
        data = FindObjectOfType<TankData>(); //Grabs object with TankData script
    }

    void FixedUpdate()
    {
        Refresh();
        //setCoordinate = target.transform.position + offset; //setCoordinate is our target plus the camera's offset position
        //smoothPosition = Vector3.Lerp(transform.position, setCoordinate, smoothOutDuration); //From the camera's position to the set Coordinate, it'll go at a rate of smoothOutDuration
        //transform.position = smoothPosition;    //Apply the modified smooth position to the camera's transform component.
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
}

