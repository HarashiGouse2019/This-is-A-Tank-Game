using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
  
    public TankData pawn;

    public GameObject target;

    public Shoot enemyShoot;

    public List<Transform> wayPoints;

    public Timer timer;

    //State the Timers used:
    //---Timer 0: Shooting Routine
    //---Timer 1: Default Finite States
    //---Timer 2: Avoid Finite State
    //---Timer 3: Attack Finite State

    public int currentWayPoint;
    public float cutoff;
    public bool isForward;
    public float stateStartTime;
    public float startAvoidTime;
    public float startAttackTime;
    public float feelerDistance;
    public SphereCollider hearingRadar;
    public float hearingDistance;

    public bool isDead = false;

    public enum LoopType { Loop, Stop, PingPong, Random };
    public enum AiStates { Idle, Patrol, Chase, Flee, Dead };
    public enum AIAvoidState { Null, TurnToAvoid, MoveToAvoid };
    public enum AiAttackState { Null, Attack };

    public LoopType looptype;
    public AiStates currentState;
    public AIAvoidState currentAvoidState = AIAvoidState.Null;
    public AiAttackState currentAttackState = AiAttackState.Null;
    public float avoidMoveTime;

    public GameObject explosionParticle;

    private void Start()
    {
        hearingRadar = GetComponentInChildren<SphereCollider>();
        currentState = AiStates.Idle;
        target = GameManager.instance.players[0].gameObject;
        GetWayPoints();
    }

    private void Update()
    {
        if (!isDead)
        {
            AIMain();
        }
    }

    protected void AIMain()
    {
        //Handle Switch States
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
            case AiStates.Dead:
                Dead();
                break;
            default:
                Idle();
                break;
        }
        switch (currentAvoidState)
        {
            case AIAvoidState.Null:
                //Do Nothing
                break;
            case AIAvoidState.MoveToAvoid:
                MoveToAvoid();
                break;
            default:
                break;
        }

        switch (currentAttackState)
        {
            case AiAttackState.Null:
                //Do Nothing
                break;
            case AiAttackState.Attack:
                Attack(target.transform);
                break;
            default:
                break;
        }

        //Check if Hearing Radar is set as trigger
        if (!hearingRadar.isTrigger)
        {
            Debug.LogWarning("The SphereCollider used for the Hearing Radar must be set to Trigger.");
        }

        if (pawn.health <= 0f)
        {
            ChangeState(AiStates.Dead);
        }
    }

    public void ChangeState(AiStates newState)
    {
        timer.StartTimer(0);
        stateStartTime = timer.currentTime[0];
        currentState = newState;

        currentAvoidState = AIAvoidState.Null;
    }

    public void ChangeAvoidState(AIAvoidState newState)
    {
        timer.StartTimer(1);
        startAvoidTime = timer.currentTime[1];
        currentAvoidState = newState;
    }

    public void ChangeAttackState(AiAttackState newState)
    {
        timer.StartTimer(3);
        startAttackTime = timer.currentTime[3];
        currentAttackState = newState;
    }

    public virtual void Idle()
    {
        //Do Nothing
    }

    public void Seek(Transform target)
    {
        if (target != null)
        {
            switch (currentAvoidState)
            {
                case AIAvoidState.Null:
                    //Chase
                    Vector3 targetVector = (target.position - pawn.bodytf.position).normalized;
                    pawn.mover.RotateTowards(targetVector);
                    pawn.mover.Move(Vector3.forward * pawn.moveSpeed * Time.deltaTime);
                    //If you are blocked, turn to avoid
                    if (IsBlocked())
                    {
                        ChangeAvoidState(AIAvoidState.TurnToAvoid);
                    }
                    break;
                case AIAvoidState.TurnToAvoid:
                    pawn.mover.Rotate(pawn.rotateSpeed * Time.deltaTime);
                    //If you are not block, move to avoid
                    if (!IsBlocked())
                    {
                        ChangeAvoidState(AIAvoidState.MoveToAvoid);
                    }
                    break;
                case AIAvoidState.MoveToAvoid:
                    //Move foward
                    pawn.mover.Move(Vector3.forward * Time.deltaTime);
                    //If time is up, there's nothing in your way
                    if (timer.currentTime[1] > startAvoidTime + avoidMoveTime)
                    {
                        timer.ResetTime(1, false);
                        ChangeAvoidState(AIAvoidState.Null);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public virtual void Patrol(Transform target)
    {

        //Start Patrolling
        Seek(target);
        if (Vector3.Distance(pawn.bodytf.position, wayPoints[currentWayPoint].position) <= cutoff)
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
    }
    public virtual bool IsBlocked()
    {
        if (Physics.Raycast(pawn.transform.position, transform.forward, out RaycastHit hit, feelerDistance))
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
        Vector3 targetVector = (pawn.transform.position - target.position); //Doing this backwards will give us a negative vector!
        Vector3 awayVector = targetVector;
        //pawn.mover.rotateTowards(awayVector);
        pawn.mover.RotateTowards(awayVector);
        pawn.mover.Move(Vector3.forward * pawn.moveSpeed * Time.deltaTime);

    }

    public virtual bool MoveToAvoid()
    {
        //Avoid objects
        if (Physics.Raycast(pawn.transform.position, transform.forward, out RaycastHit hit, feelerDistance))
        {
            if (hit.collider.tag == "Obstacle")
            {
                pawn.mover.Rotate(pawn.rotateSpeed * Time.deltaTime);
                return true;
            }
            else
            {
                ChangeState(AiStates.Patrol); //This will become a potential problem
                ChangeAvoidState(AIAvoidState.Null);
                return false;
            }
        }

        return false;
    }
    public virtual void Chase(Transform target)
    {
        //Chase
        Vector3 targetVector = (target.position - pawn.bodytf.position ).normalized;
        pawn.mover.RotateTowards(targetVector);
        pawn.mover.Move(Vector3.forward * pawn.moveSpeed * Time.deltaTime);
    }
    public virtual void Attack(Transform target)
    {
        enemyShoot.InitiateEnemyControls(pawn.shotsPerSecond); //Start basic enemy routine
    }
    public virtual void Dead()
    {
        if (isDead == false)
        {
            Instantiate(explosionParticle, pawn.gameObject.transform.position, pawn.gameObject.transform.rotation);
            GameManager.instance.enemyKilled++;
            isDead = true;
            Destroy(pawn.gameObject);
        }
    }
    public virtual bool CanSee()
    {
        //Can see will produce a raycast foward
        Debug.DrawRay(pawn.bodytf.position, transform.forward * feelerDistance, Color.red);
        if (Physics.Raycast(pawn.transform.position, pawn.transform.forward, out RaycastHit hit, feelerDistance))
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
    public virtual bool CanHear()
    {
        
        switch(hearingRadar.isTrigger)
        {
            case false:
                Debug.LogWarning("The collider that is used as the hearing distance must be a trigger.");
                break;
            case true:
                if (GetComponentInChildren<RadarDetection>().playerDetected)
                {
                    Debug.Log("I hear'ya boj!");
                    return true;
                } 
                break;
        }
        //Can hear is going to take advantage of using a circle collider that is set as a trigger
        return false;
    }
    public virtual void GetWayPoints()
    {
        if (wayPoints.Count == 0)
        {
            wayPoints = new List<Transform>();
            for (int i = 0; i < GameManager.instance.progen.scan.wayPoints.Count; i++)
            {
                wayPoints.Add(GameManager.instance.progen.scan.wayPoints[i].transform);
            }
        }
    }

    public void ClearOut()
    {
        //We destory our object
        Destroy(gameObject);
    }
}
