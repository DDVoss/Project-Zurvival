using UnityEngine;


public class PlayerLocomotionState : PlayerState
{
    public PlayerLocomotionState(PlayerContext context, PlayerStateMachine.EPlayerState estate) : base(context, estate)
    {
    }
    
    public override void EnterState() { Debug.Log("Entering locomotion State"); }

    public override void ExitState() { Debug.Log("Exiting locomotion State"); }

   public override void UpdateState()
{
    
    ApplyGravity();

    Vector3 cameraForwardXZ = new Vector3(Context.PlayerCamera.transform.forward.x, 0f,
        Context.PlayerCamera.transform.forward.z).normalized;
    Vector3 cameraRightXZ = new Vector3(Context.PlayerCamera.transform.right.x, 0f,
        Context.PlayerCamera.transform.right.z).normalized;
    Vector3 movementDirection = cameraRightXZ * Context.PlayerLocomotionInput.MovementInput.x +
                                cameraForwardXZ * Context.PlayerLocomotionInput.MovementInput.y;

    if (movementDirection.magnitude > 0.1f)
    {
        float directionDifference = Vector3.Angle(Context.PreviousMovementDirection, movementDirection);
        if (directionDifference > Context.TurningDegrees)
            Context.DirectionChangeTimer = Context.DirectionChangeDelay;

        Context.PreviousMovementDirection = movementDirection;

        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        Context.PlayerTransform.rotation = Quaternion.Slerp(
            Context.PlayerTransform.rotation, targetRotation, Context.RotationSpeed * Time.deltaTime);
    }

    if (Context.DirectionChangeTimer > 0f)
        Context.DirectionChangeTimer -= Time.deltaTime;

    float movementMultiplier = Context.DirectionChangeTimer > 0f ? 0f : 1f;

    Vector3 movementDelta = movementDirection * Context.RunAcceleration * Time.deltaTime * movementMultiplier;
    Vector3 newVelocity = Context.CharacterController.velocity + movementDelta;

    Vector3 currentDrag = newVelocity.normalized * Context.Drag * Time.deltaTime;
    newVelocity = (newVelocity.magnitude > Context.Drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
    newVelocity = Vector3.ClampMagnitude(newVelocity, Context.RunSpeed);
    

    Vector3 totalMovement = newVelocity * Time.deltaTime;
    totalMovement.y = Context.PlayerVelocity.y * Time.deltaTime;

    Context.CharacterController.Move(totalMovement);
}


    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        // When player presses aim, transition to aiming state
        if (Context.PlayerLocomotionInput.AimInput)
        {
            return PlayerStateMachine.EPlayerState.Aiming;
        }
        return StateKey; // stay in this state by default
    }

    public override void OnTriggerEnter(Collider other) { }

    public override void OnTriggerStay(Collider other) { }

    public override void OnTriggerExit(Collider other) { }

}