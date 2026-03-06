using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerContext context, PlayerStateMachine.EPlayerState estate) : base(context, estate)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entering attack State");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting attack State");
    }

    public override void UpdateState()
    {
        ApplyGravity();
        // Attack logic would go here, for now we just log it
        
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (!Context.PlayerLocomotionInput.AttackInput)
        {
            return PlayerStateMachine.EPlayerState.Aiming;
        }
        return StateKey; // stay in this state by default
    }

    public override void OnTriggerEnter(Collider other) { }

    public override void OnTriggerStay(Collider other) { }

    public override void OnTriggerExit(Collider other) { }
}