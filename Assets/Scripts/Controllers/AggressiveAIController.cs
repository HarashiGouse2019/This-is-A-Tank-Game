using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveAIController : AIController
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
        base.Idle();
    }
    public override void Patrol(Transform target)
    {
        //Chase the player if you see him
        if (CanHear())
        {
            ChangeState(AiStates.Chase);
        }
        //If something's in your way, avoid it
        if (IsBlocked())
        {
            ChangeAvoidState(AIAvoidState.MoveToAvoid);
        }

        base.Patrol(target);
    }
    public override void Chase(Transform target)
    {
        //If close to the player, attack him
        if (Vector3.Distance(pawn.transform.position, target.position) < 10)
        {
            ChangeAttackState(AiAttackState.Attack);
        }
        base.Chase(target);
    }
    public override void Attack(Transform target)
    {
        //Continue attacking unless not in range
        if (CanSee())
        {
            ChangeState(AiStates.Chase);
        }
        //Continue attacking unless obstacles in the way
        bool isBlocked = IsBlocked();
        if (isBlocked)
        {
            ChangeAvoidState(AIAvoidState.MoveToAvoid);
        }
        //Do not flee if HP is low
        //Keep pursuing the player
        base.Attack(target);
    }
    public override bool MoveToAvoid()
    {
        //Move out of obstacles
        //You will not be fleeing
        return base.MoveToAvoid();
    }
    public override void Dead()
    {
        //You have died. You can't do anything
        base.Dead();
    }
}
