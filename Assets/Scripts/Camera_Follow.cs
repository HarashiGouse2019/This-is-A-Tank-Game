using System.Collections;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public static Camera_Follow camerafollow;

    //The Targeted GameObject to manipulate its position through it's Transform Component
    public GameObject target;

    //Used to set the duration of the camera smoothing out and in towards the player
    public float smoothOutDuration = 0.125f;

    public float camRotationSpeed = 1f;

    public float radius;

    //Setting the offset of the camera
    public Vector3 offset;

    private Vector3 center;

    public float angleValue = 0;

    void Start()
    {
        target = FindObjectOfType<TankMover>().gameObject;
    }

    void FixedUpdate()
    {
        Vector3 setCoordinate;
        Vector3 smoothPosition;
        setCoordinate = target.transform.position + offset;
        smoothPosition = Vector3.Lerp(transform.position, setCoordinate, smoothOutDuration);
        transform.position = smoothPosition;                                                                         
    }

    public void Rotate(float direction)
    {
        transform.Rotate(0, camRotationSpeed * direction, 0);
    }
}

