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

    public float radius; //The radius of camera

    //Setting the offset of the camera
    public Vector3 offset;

    private Vector3 center; //The Center

    public float angleValue = 0; //Angle Value in which to apply to camera rotation

    void Start()
    {
        //FindObjectOfType interates through all of the known gameobjects, and returns the first one with a defined component.
        //That is why you should use it in the start or awake functions
        target = FindObjectOfType<TurretMover>().gameObject; //Grabs the object with TurretMover script
        data = FindObjectOfType<TankData>(); //Grabs object with TankData script
    }

    void FixedUpdate()
    {
        Vector3 setCoordinate; //Set up camera positioning
        Vector3 smoothPosition; //This position is use to create a smooth effect for our camera
        setCoordinate = target.transform.position + offset; //setCoordinate is our target plus the camera's offset position
        smoothPosition = Vector3.Lerp(transform.position, setCoordinate, smoothOutDuration); //From the camera's position to the set Coordinate, it'll go at a rate of smoothOutDuration
        transform.position = smoothPosition;    //Apply the modified smooth position to the camera's transform component.                                                                     
    }

    //Rotating the camera
    public void Rotate(float direction)
    {
        angleValue += direction * camRotationSpeed; //the Angle value will go at an increment of the set direction times the speed of the camera
        transform.LookAt(target.transform); //Have the camera look at the target(which is the turret)
        transform.Translate(new Vector3(-direction, 0, 0) * camRotationSpeed * Time.deltaTime); //Translate the camera
    }
}

