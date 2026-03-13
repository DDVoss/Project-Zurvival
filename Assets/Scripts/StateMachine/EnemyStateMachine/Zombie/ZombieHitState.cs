using UnityEngine;

public class ZombieHitState : ZombieState
{
    public ZombieHitState(ZombieContext context, ZombieStateMachine.EZombieState estate) : base(context, estate)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entering hit State");
    }
    
    public override void ExitState() { Debug.Log("Exiting hit State"); }

    public override void UpdateState()
    {
        // Implement hit reaction behavior here (e.g., play hit animation, apply knockback)
        
    }

    public override ZombieStateMachine.EZombieState GetNextState()
    {
     
        float distanceToTarget = Vector3.Distance(Context.Agent.transform.position, Context.Target.position);

        if (distanceToTarget <= Context.AttackRange)
            return ZombieStateMachine.EZombieState.Attack;
        if (distanceToTarget <= Context.ChaseRange)
            return ZombieStateMachine.EZombieState.Chase;
        else
            return ZombieStateMachine.EZombieState.Patrol;
    }


    public override void OnTriggerEnter(Collider other) { }

    public override void OnTriggerStay(Collider other) { }

    public override void OnTriggerExit(Collider other) { }

    public void ReceiveDamage(float amount)
    {
        Context.Health -= amount;
    }
}