using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CunningAIController : AIController
{
    // Start is called before the first frame update
    void Start()
    {
        currentWayPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        AIMain();
    }
    public override void Idle()
    {
        //Head into patrol
        ChangeState(AiStates.Patrol);
        //During low health, if player is nearby, start attacking
        if (pawn.health < 25f / pawn.maxHealth || CanHear())
        {
            ChangeAttackState(AiAttackState.Attack);
        }
        base.Idle();
    }
    public override void Patrol(Transform target)
    {
        //Patrol in the normal route.
        //Is enemy is close by, flee, unless the player is low on health
        if (CanHear())
        {
            if (pawn.health <= 25f)
            {
                ChangeState(AiStates.Flee);
            }
            else
            {
                ChangeState(AiStates.Chase);
            }
        }

        if (CanSee())
        {
            ChangeAttackState(AiAttackState.Attack);
        }

        if (IsBlocked())
        {
            ChangeAvoidState(AIAvoidState.MoveToAvoid);
        }

        base.Patrol(target);
    }

    public override void Chase(Transform target)
    {
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
        //Attack when the player is low on health
        //However, if you are low, flee
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
        base.Attack(target);
    }
    public override void Flee(Transform target)
    {
        //Check if there's anything block your way as your fleeing
        bool isBlocked = IsBlocked();
        if (isBlocked)
        {
            ChangeAvoidState(AIAvoidState.MoveToAvoid);
        }

        //Flee until you are at a certain distance away from the player.
        if (!CanHear())
        {
            if (pawn.health < 25f / pawn.maxHealth)
            {
                ChangeState(AiStates.Idle);
            } else
            {
                ChangeState(AiStates.Patrol);
            }
        }
        //If low on health, stop, and predicts player's position.
        //If player is in predicted area, attack
        //Otherwise, stay idle
        //If player gets too close, start to flee
        base.Flee(target);
    }
    public override bool MoveToAvoid()
    {
        //Move away from anything that in your way.
        return base.MoveToAvoid();
    }
    public override void Dead()
    {
        //You are dead. You can't continue.
        base.Dead();
    }
}
