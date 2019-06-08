using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAIController : AIController
{

    private void Start()
    {
        currentWayPoint = 0;
    }

    private void Update()
    {
        switch(currentState)
        {
            case AiStates.Idle:
                //Do a special update
                //Check for transitions
                if (timer.currentTime > stateStartTime + 3f)
                {
                    timer.ResetTime(true);
                    ChangeState(AiStates.Patrol);
                }
                break;
            case AiStates.Patrol:
                //Do a special update
                Patrol();
                //Check For Transitions
                // TODO: Add some transitions
                break;



        }
    }

    private void Patrol()
    {
        AISeekUpdate(wayPoints[currentWayPoint]);
        if (Vector3.Distance(pawn.transform.position, wayPoints[currentWayPoint].position) <= cutoff)
        {
            if (isForward) {
                currentWayPoint++;
            } else {
                currentWayPoint--;
            }
            

            if (currentWayPoint >= wayPoints.Count || currentWayPoint < 0)
            {
                if (looptype == LoopType.Loop)
                {
                    currentWayPoint = 0;

                } else if (looptype == LoopType.Random)
                {
                    currentWayPoint = Random.Range(0, wayPoints.Count);
                } else if (looptype == LoopType.PingPong)
                {
                    isForward = !isForward;
                    if (currentWayPoint >= wayPoints.Count)
                    {
                        currentWayPoint = wayPoints.Count - 1;
                    } else
                    {
                        currentWayPoint = 0;
                    }
                }
            }
        }
    }
}
