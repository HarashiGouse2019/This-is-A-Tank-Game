using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public TankData pawn;
    public TankMover tankMover;
    public new Camera_Follow camera;

    Transform pawnTf;

    // Start is called before the first frame update
    void Start()
    {
        pawnTf = pawn.GetComponent<Transform>();
        tankMover = pawn.GetComponent<TankMover>();
        camera = camera.GetComponent<Camera_Follow>();
    }

    // Update is called once per frame
    void Update()
    {
        //Start with the assumption that I'm not moving
        Vector3 directionToMove = Vector3.zero;

        // If thw E key is down -- add "forward" to the direction I'm moving
        if(Input.GetKey(KeyCode.W))
        {
            directionToMove += Vector3.forward;
        }

        // If S key is down -- Add "reverse" to the direction I am moving
        if (Input.GetKey(KeyCode.S))
        {
            directionToMove += -Vector3.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            pawn.mover.Rotate(-pawn.rotateSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            pawn.mover.Rotate(pawn.rotateSpeed);
        }

        //Camera Controls with arrow keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            camera.Rotate(1);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            camera.Rotate(-1);
        }

        // After I've checked all my imputs, tell my mover to move in the final direction;s
        pawn.mover.Move(directionToMove);

        //gameObject.SendMessage("Move", (Vector3)directionToMove, SendMessageOptions.RequireReceiver); // Go through all the components with this function. If that component has a Move function, tell that component to run it.
        //                                                                    //If all components do not have this function, the second parameter will say that it's okay that you don't have one.
    }
}
