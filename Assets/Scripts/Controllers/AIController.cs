using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public TankData pawn;
    public TankData target;
    public List<Transform> wayPoints;
    public Timer timer;

    public int currentWayPoint;
    public float cutoff;
    public bool isForward;
    public float stateStartTime;
    public float feelerDistance;

    public bool isDead = false;

    public enum LoopType { Loop, Stop, PingPong, Random };
    public enum AiStates { Idle, Patrol, MoveToAvoid, Chase, Flee, Attack, Dead };

    public LoopType looptype;
    public AiStates currentState;

    private void Start()
    {
        currentState = AiStates.Idle;
    }

    private void Update()
    {
        if (!isDead)
        {
            handleStateSwitches();
        }
        if (pawn.health <= 0)
        {
            ChangeState(AiStates.Dead);
        }
    }

    protected void handleStateSwitches()
    {
        switch (currentState)
        {
            case AiStates.Idle:
                Idle();
                break;
            case AiStates.Patrol:
                Patrol(wayPoints[currentWayPoint]);
                break;
            case AiStates.Chase:
                Chase(target.transform);
                break;
            case AiStates.Flee:
                Flee(target.transform);
                break;
            case AiStates.Attack:
                Attack(target.transform);
                break;
            case AiStates.Dead:
                Dead();
                break;
            default:
                Idle();
                break;
        }
    }

    public void ChangeState(AiStates newState)
    {
        timer.StartTimer();
        stateStartTime = timer.currentTime;
        currentState = newState;
    }

    public virtual void Idle()
    {
        //Do Nothing
    }

    public virtual void Patrol(Transform target)
    {
        
    }
    public virtual bool IsBlocked()
    {
        RaycastHit hit;
        if (Physics.Raycast(pawn.transform.position, transform.forward, out hit, feelerDistance))
        {
            if (hit.collider.tag == "Obstacle")
            {
                return true;
            }
        }
        return false;
    }
    public virtual void Flee(Transform target)
    {
        Vector3 targetVector = (target.position - pawn.transform.position);
        Vector3 awayVector = -targetVector;
        //pawn.mover.rotateTowards(awayVector);
        pawn.mover.Move(Vector3.forward);
    }
    public virtual bool MoveToAvoid()
    {
        //Avoid objects
        RaycastHit hit;
        if (Physics.Raycast(pawn.transform.position, transform.forward, out hit, feelerDistance))
        {
            if (hit.collider.tag == "Obstacle")
            {
                if (Vector3.Distance(pawn.transform.position, hit.collider.gameObject.transform.position) < 10)
                {
                    pawn.mover.Rotate(pawn.rotateSpeed * Time.deltaTime);
                }
                else if (Vector3.Distance(pawn.transform.position, hit.collider.gameObject.transform.position) < 5)
                {
                    pawn.mover.Rotate((pawn.rotateSpeed * 2) * Time.deltaTime);
                }
                else if (Vector3.Distance(pawn.transform.position, hit.collider.gameObject.transform.position) < 2)
                {
                    pawn.mover.Rotate((pawn.rotateSpeed * 3) * Time.deltaTime);
                }
                return true;
            }
        }
        else
        {
            ChangeState(AiStates.Patrol); //This will become a potential problem
            return false;
        }
        return false;
    }
    public virtual void Chase(Transform target)
    {

    }
    public virtual void Attack(Transform target)
    {

    }
    public virtual void Dead()
    {
        isDead = true;
    }
    public virtual bool CanSee()
    {
        return false;
    }
    public virtual bool CanHear()
    {
        return false;
    }
}
