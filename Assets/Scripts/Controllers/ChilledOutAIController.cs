using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChilledOutAIController : AIController
{
    // Start is called before the first frame update
    void Start()
    {
        currentWayPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        handleStateSwitches();
    }

    public override void Idle()
    {
        //Go into patrol
        ChangeState(AiStates.Patrol);
        base.Idle();
    }
    public override void Patrol(Transform target)
    {
        RaycastHit hit;
        //If player shoots at us, transition to Attack
        //Otherwise, check if there's anything blocking our way.
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
        //Continue shooting the player. If however your HP is low, start to flee.
        if (pawn.health <= 25f / pawn.maxHealth)
        {
            ChangeState(AiStates.Flee);
        }
        base.Attack(target);
    }
    public override void Flee(Transform target)
    {
        //Check to see if you have anything in the way while going back
        bool isBlocked = IsBlocked();
        if (isBlocked)
        {
            ChangeState(AiStates.MoveToAvoid);
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
