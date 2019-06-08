using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public TankData pawn;
    public List<Transform> wayPoints;
    public Timer timer;

    public int currentWayPoint;
    public float cutoff;
    public bool isForward;
    public float stateStartTime;
    public float feelerDistance;

    public enum LoopType { Loop, Stop, PingPong, Random };
    public enum AiStates { Idle, Patrol, TurnToAvoid, MoveToAvoid, Chase, Flee };

    public LoopType looptype;
    public AiStates currentState;

    public void ChangeState(AiStates newState)
    {
        timer.StartTimer();
        stateStartTime = timer.currentTime;
        currentState = newState;
    }

    public void Idle()
    {
        //Do Nothing
    }

    public bool IsBlocked()
    {
        if (Physics.Raycast(pawn.transform.position, pawn.transform.forward, feelerDistance))
        {
            ChangeState(AiStates.TurnToAvoid);
            return true;
        }
        return false;
    }

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
