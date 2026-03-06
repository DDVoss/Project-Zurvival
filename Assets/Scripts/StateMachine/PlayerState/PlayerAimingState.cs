using UnityEngine;

public class PlayerAimingState : PlayerState
{
    float _aimingRotationSpeed = 100f; // Hardcoded for now, can be made configurable later
    public PlayerAimingState(PlayerContext context, PlayerStateMachine.EPlayerState estate) : base(context, estate)
    {
    }

    public override void EnterState() { Debug.Log("Entering Aiming State"); }

    public override void ExitState() { Debug.Log("Exiting Aiming State"); }

    public override void UpdateState()
    {
        ApplyGravity();
        float rotationInput = Context.PlayerLocomotionInput.MovementInput.x;
        Context.PlayerTransform.Rotate(Vector3.up, rotationInput * _aimingRotationSpeed * Time.deltaTime);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (!Context.PlayerLocomotionInput.AimInput)
        {
            return PlayerStateMachine.EPlayerState.Locomotion;
        } 
        if (Context.PlayerLocomotionInput.AttackInput)
        {
            return PlayerStateMachine.EPlayerState.Attacking;
        }
        
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}