using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObedienteAIController : AIController
{

    private void Start()
    {
        currentWayPoint = 0;
    }

    private void Update()
    {
        handleStateSwitches();
    }

    public override void Idle()
    {
        //Head into patrol
        ChangeState(AiStates.Patrol);
        base.Idle();
    }
    public override void Patrol(Transform target)
    {
        //Start Patrolling
        Vector3 targetVector = (target.position - pawn.transform.position).normalized;

        pawn.mover.Move(Vector3.forward);

        target = wayPoints[currentWayPoint];

        if (Vector3.Distance(pawn.transform.position, wayPoints[currentWayPoint].position) <= cutoff)
        {
            if (isForward)
            {
                currentWayPoint++;
            }
            else
            {
                currentWayPoint--;
            }


            if (currentWayPoint >= wayPoints.Count || currentWayPoint < 0)
            {
                if (looptype == LoopType.Loop)
                {
                    currentWayPoint = 0;

                }
                else if (looptype == LoopType.Random)
                {
                    currentWayPoint = Random.Range(0, wayPoints.Count);
                }
                else if (looptype == LoopType.PingPong)
                {
                    isForward = !isForward;
                    if (currentWayPoint >= wayPoints.Count)
                    {
                        currentWayPoint = wayPoints.Count - 1;
                    }
                    else
                    {
                        currentWayPoint = 0;
                    }
                }
            }
        }

        RaycastHit hit;
        //If you detect the player nearby, attack.
        //It will remain in it's spot until you die or the player dies.
        //Otherwise, check if there's anything in your way
        if (Physics.Raycast(pawn.transform.position, transform.forward, out hit, feelerDistance))
        {
            if (hit.collider.tag == "Player")
            {
                ChangeState(AiStates.Attack);
            }
            else if (hit.collider.tag == "Obstacle")
            {
                ChangeState(AiStates.MoveToAvoid); 
            }
        }
        base.Patrol(target);
    }
    public override void Attack(Transform target)
    {
        //Flee if you are low on HP
        if (pawn.health < 25f / pawn.maxHealth)
        {
            ChangeState(AiStates.Flee);
        }
        base.Attack(target);
    }
    public override void Flee(Transform target)
    {
        //Check if there's anything block your way as your fleeing
        bool isBlocked = IsBlocked();
        if (isBlocked)
        {
            ChangeState(AiStates.MoveToAvoid);
        }
        base.Flee(target);
    }
    public override bool MoveToAvoid()
    {
        return base.MoveToAvoid();
    }
    public override void Dead()
    {
        //You are died! You can not proceed.
       
        base.Dead();
    }
}
