using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public TankData pawn;

    public void AISeekUpdate(Transform target)
    {
        Vector3 targetVector = (target.position - pawn.transform.position).normalized;
        //pawn.mover.RotateTowards(targetVector);
        pawn.mover.Move(Vector3.forward);
    }
    public void AIFlee(Transform target)
    {
        Vector3 targetVector = (target.position - pawn.transform.position);
        Vector3 awayVector = -targetVector;
        //pawn.mover.rotateTowards(awayVector);
        pawn.mover.Move(Vector3.forward);
    }
}
