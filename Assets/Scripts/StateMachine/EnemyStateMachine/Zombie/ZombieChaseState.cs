using UnityEngine;

public class ZombieChaseState : ZombieState
{
    public ZombieChaseState(ZombieContext context, ZombieStateMachine.EZombieState estate) : base(context, estate)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entering Chase State"); 
            Context.Agent.speed = Context.ChaseSpeed; // Set the agent's speed to the chase speed
    }

    public override void ExitState() { Debug.Log("Exiting Chase State"); }

    public override void UpdateState()
    {
        Context.Agent.SetDestination(Context.Target.position); // Move towards the target
    }

    public override ZombieStateMachine.EZombieState GetNextState()
    {
        if (Context.IsHit)
        {
            Context.IsHit = false;
            return ZombieStateMachine.EZombieState.Hit;
        }
        
        float distanceToTarget = Vector3.Distance(Context.Agent.transform.position, Context.Target.position);
        if (distanceToTarget <= Context.AttackRange)
        {
            return ZombieStateMachine.EZombieState.Attack;
            
        } else if (distanceToTarget > Context.ChaseRange)
        {
            return ZombieStateMachine.EZombieState.Patrol;
        }
        
        return StateKey; // stay in this state by default
    }

    public override void OnTriggerEnter(Collider other) { }

    public override void OnTriggerStay(Collider other) { }

    public override void OnTriggerExit(Collider other) { }
}
