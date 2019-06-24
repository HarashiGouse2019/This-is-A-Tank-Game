using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public CharacterController characterController; //Reference our character controller
    public Transform tf; //Reference our transform
    public TankData data; //Reference our tank data component

    private void Start()
    {
        tf = GetComponent<Transform>(); //Grab our tranform and tank data
        data = GetComponent<TankData>();
    }
    public void Move(Vector3 worldDirectionToMove)
    {
        //Calculate our direction based on our rotation (so 0,0,1 becomes our forward)
        Vector3 directionToMove = data.bodytf.TransformDirection(worldDirectionToMove);

        //Actually move
        characterController.SimpleMove(directionToMove);

    }

    public void Rotate(float direction)
    {
        data.bodytf.Rotate(new Vector3(0, direction * data.rotateSpeed * Time.deltaTime, 0)); //Rotate in a set direction and speed over time on the y axis 
    }

    public void RotateTowards(Vector3 lookVector)
    {
        //Find vector to target
        Vector3 vectorToTarget = new Vector3 (lookVector.x, 0f, lookVector.z);

        //Find quarternion to look down that vector
        Quaternion targetQuaternion = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        //Set our rotation to "partway towards" that quaternion
        data.bodytf.rotation = Quaternion.RotateTowards(data.bodytf.rotation, targetQuaternion, data.rotateSpeed * Time.deltaTime);
    }
}
