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
        AIMain();
    }

    public override void Idle()
    {
        //Head into patrol
        ChangeState(AiStates.Patrol);
        base.Idle();
    }
    public override void Patrol(Transform target)
    {
        if (CanHear())
        {
            ChangeState(AiStates.Chase);
        }

        //Otherwise, avoid any obstacles
        if (IsBlocked())
        {
            ChangeAvoidState(AIAvoidState.MoveToAvoid);
        }

        base.Patrol(target);
    }

    public override void Chase(Transform target)
    {
        //Attack the player if you see him
        if (CanSee())
        {
            ChangeAttackState(AiAttackState.Attack);
        }

        if (!CanSee() && !CanHear())
        {
            ChangeState(AiStates.Patrol);
            ChangeAttackState(AiAttackState.Null);
        }

        base.Chase(target);
    }

    public override void Attack(Transform target)
    {
        //Flee if you are low on HP
        if (pawn.health < 25f / pawn.maxHealth)
        {
            ChangeState(AiStates.Flee);
            ChangeAttackState(AiAttackState.Null);
        }

        if (!CanSee())
        {
            ChangeState(AiStates.Chase);
            ChangeAttackState(AiAttackState.Null);
        }

        if (!CanHear())
        {
            ChangeState(AiStates.Patrol);
        }

        base.Chase(target); //We still what our guys chasing us!
        base.Attack(target); //We use the base and override it.

        //Doing these two things prevents the attack and chase states from switching back and forth to different states!
    }
    public override void Flee(Transform target)
    {
        //Check if there's anything block your way as your fleeing
        bool isBlocked = IsBlocked();
        if (isBlocked)
        {
            ChangeAvoidState(AIAvoidState.MoveToAvoid);
        }

        //If he doesn't hear me at a certain distance, head back to patrolling!!!
        if (!CanHear())
        {
            ChangeState(AiStates.Patrol);
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
