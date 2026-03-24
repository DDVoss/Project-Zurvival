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
    
    ApplyGravity(); // From the base PlayerState, applies gravity and updates grounded status
    
    // Get the camera's forward and right vectors, ignoring the y component for horizontal movement
    // Zeroes out the y component to prevent movement if camera is looking up or down
    Vector3 cameraForwardXZ = new Vector3(Context.PlayerCamera.transform.forward.x, 0f,
        Context.PlayerCamera.transform.forward.z).normalized; // Normalize converts to unit vector to ensure consistent movement speed
    Vector3 cameraRightXZ = new Vector3(Context.PlayerCamera.transform.right.x, 0f,
        Context.PlayerCamera.transform.right.z).normalized;
    
    // Calculate the desired movement direction based on player input and camera orientation
    Vector3 movementDirection = cameraRightXZ * Context.PlayerLocomotionInput.MovementInput.x +
                                cameraForwardXZ * Context.PlayerLocomotionInput.MovementInput.y;

    // Magnitude is the distance from the origin (0,0,0) to the point represented by the vector
    // If the magnitude is greater than 0.1, it means the player is providing significant input to move in a direction
    if (movementDirection.magnitude > 0.1f)
    {
        // Check if the player has changed direction significantly since the last frame
        float directionDifference = Vector3.Angle(Context.PreviousMovementDirection, movementDirection);
        
        // If the player has turned more than the Context.TurningDegrees threshold, reset the direction change timer to apply movement delay
        if (directionDifference > Context.TurningDegrees)
            Context.DirectionChangeTimer = Context.DirectionChangeDelay;
        
        Context.PreviousMovementDirection = movementDirection;

        // Rotate the player to face the movement direction using Slerp for smooth rotation
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        
        // Slerp smooths rotation instead of snapping to the target rotation
        Context.PlayerTransform.rotation = Quaternion.Slerp(
            Context.PlayerTransform.rotation, targetRotation, Context.RotationSpeed * Time.deltaTime);
    }
    
    // Count down the direction change timer if it's active
    if (Context.DirectionChangeTimer > 0f)
        Context.DirectionChangeTimer -= Time.deltaTime;

    // If the timer is active, set movement multiplier to 0 to prevent movement during direction change delay, otherwise set to 1 for normal movement
    float movementMultiplier = Context.DirectionChangeTimer > 0f ? 0f : 1f;

    // Apply acceleration to the player's velocity based on the movement direction
    Vector3 movementDelta = movementDirection * Context.RunAcceleration * Time.deltaTime * movementMultiplier;
    Vector3 newVelocity = Context.CharacterController.velocity + movementDelta;

    // Apply drag to slow down the player when there is no input, or when changing directions
    Vector3 currentDrag = newVelocity.normalized * Context.Drag * Time.deltaTime;
    newVelocity = (newVelocity.magnitude > Context.Drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
    newVelocity = Vector3.ClampMagnitude(newVelocity, Context.RunSpeed); // Clamp the velocity to the maximum run speed
    
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