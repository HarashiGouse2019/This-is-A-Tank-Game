using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public CharacterController characterController;
    public Transform tf;
    public TankData data;

    private void Start()
    {
        tf = GetComponent<Transform>();
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
        data.bodytf.Rotate(new Vector3(0, direction * data.rotateSpeed * Time.deltaTime, 0));
    }
}
