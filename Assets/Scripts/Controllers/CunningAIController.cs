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
        handleStateSwitches();
    }
    public override void Idle()
    {
        //Head into patrol
        base.Idle();
    }
    public override void Patrol(Transform target)
    {
        //Patrol in the normal route.
        //Is enemy is close by, flee, unless the player is low on health
        base.Patrol(target);
    }
    public override void Attack(Transform target)
    {
        //Attack when the player is low on health
        //However, if you are low, flee.
        base.Attack(target);
    }
    public override void Flee(Transform target)
    {
        //Flee until you are at a certain distance away from the player.
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
