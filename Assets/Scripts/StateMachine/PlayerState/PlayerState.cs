using UnityEngine;

public class PlayerState : BaseState<PlayerStateMachine.EPlayerState>
{
    protected PlayerContext Context;
    
    public PlayerState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(stateKey)
    {
        Context = context;
    }

    public override void EnterState() {}

    public override void ExitState() {}

    public override void UpdateState() {}

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other) {}

    public override void OnTriggerStay(Collider other) {}

    public override void OnTriggerExit(Collider other) {}

    // Common method to apply gravity, can be called from any state that needs it
    public void ApplyGravity()
    {
        Context.GroundedPlayer = Context.CharacterController.isGrounded;
        if (Context.GroundedPlayer && Context.PlayerVelocity.y < -2f)
        {
            Context.SetVelocityY(-2f);
        }
        // Apply gravity always
        Context.AddVelocityY(Context.GravityValue * Time.deltaTime);
    }
}