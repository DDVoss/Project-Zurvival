using UnityEngine;

public class ZombiePatrolState : ZombieState
{
    public ZombiePatrolState(ZombieContext context, ZombieStateMachine.EZombieState estate) : base(context, estate)
    {
    }


    public override void EnterState() { Debug.Log("Entering Patrol State"); }

    public override void ExitState() { Debug.Log("Exiting Patrol State"); }

    public override void UpdateState()
    {
        // Implement patrol behavior here (e.g., move between waypoints)
    }

    public override ZombieStateMachine.EZombieState GetNextState()
    {
        if (Context.IsHit)
        {
            Context.IsHit = false;
            return ZombieStateMachine.EZombieState.Hit;
        }
        
        float distanceToTarget = Vector3.Distance(Context.Agent.transform.position, Context.Target.position);
        if (distanceToTarget <= Context.ChaseRange)
        {            
            return ZombieStateMachine.EZombieState.Chase;
        }
        
        return StateKey; // stay in this state by default
    }

    public override void OnTriggerEnter(Collider other) { }

    public override void OnTriggerStay(Collider other) { }

    public override void OnTriggerExit(Collider other) { }
    
}
