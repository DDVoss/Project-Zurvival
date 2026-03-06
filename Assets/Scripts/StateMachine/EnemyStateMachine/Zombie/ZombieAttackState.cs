using UnityEngine;

public class ZombieAttackState : ZombieState
{
    public ZombieAttackState(ZombieContext context, ZombieStateMachine.EZombieState estate) : base(context, estate)
    {
    }
    
    public override void EnterState() { Debug.Log("Entering attack State"); }

    public override void ExitState() { Debug.Log("Exiting attack State"); }

    public override void UpdateState()
    {
        // Implement attack behavior here (e.g., play attack animation, deal damage to the player)
    }

    public override ZombieStateMachine.EZombieState GetNextState()
    {
        return StateKey; // stay in this state by default
    }

    public override void OnTriggerEnter(Collider other) { }

    public override void OnTriggerStay(Collider other) { }

    public override void OnTriggerExit(Collider other) { }
}
