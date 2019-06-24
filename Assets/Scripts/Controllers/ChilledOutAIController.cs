using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChilledOutAIController : AIController
{

    // Start is called before the first frame update
    void Start()
    {
        currentWayPoint = 0; //Start at waypoint 0
    }

    // Update is called once per frame
    void Update()
    {
        //Handles Switching States
        AIMain();
    }

    public override void Idle()
    {
        //Go into patrol
        ChangeState(AiStates.Patrol);
        base.Idle();
    }
    public override void Patrol(Transform target)
    {
        //Attack The player if you see him
        if (CanHear())
        {
            //Flee if my health is low
            if (pawn.health <= 25f)
            {
                ChangeState(AiStates.Flee);
            }
            //Otherwise, continue chasing
            else
            {
                ChangeState(AiStates.Chase);
            }
        }

        //However, avoid any obstacles
        if (IsBlocked())
        {
            ChangeAvoidState(AIAvoidState.MoveToAvoid);
        }

        base.Patrol(target);
    }

    public override void Chase(Transform target)
    {
        //If he sees me at his sights, shoot!!!
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
        //Continue shooting the player. If however your HP is low, start to flee.
        if (pawn.health <= 25f)
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
        base.Chase(target);
        base.Attack(target);
    }
    public override void Flee(Transform target)
    {
        //Check to see if you have anything in the way while going back
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
        //Avoid any objects that are in fron of you
        return base.MoveToAvoid();
    }
    public override void Dead()
    {
        //Your HP is depleted. You can't go any further.
        base.Dead();
    }
}
