using UnityEngine;

public class PlayerContext
{
    // Components (readonly)
    public Transform PlayerTransform { get; }
    public CharacterController CharacterController { get; }
    public Camera PlayerCamera { get; }
    public PlayerLocomotionInput PlayerLocomotionInput { get; }
    public Transform MuzzlePointTransform { get; } 

    // Config (readonly)
    public float RunAcceleration { get; }
    public float RunSpeed { get; }
    public float Drag { get; }
    public float GravityValue { get; }
    public float RotationSpeed { get; }
    public float DirectionChangeDelay { get; }
    public float TurningDegrees { get; }

    // Runtime state (mutable)
    public Vector3 PlayerVelocity { get; set; }
    public bool GroundedPlayer { get; set; }
    public Vector3 PreviousMovementDirection { get; set; }
    public float DirectionChangeTimer { get; set; }

    public PlayerContext(
        Transform playerTransform,
        CharacterController characterController,
        Camera playerCamera,
        PlayerLocomotionInput playerLocomotionInput,
        Transform muzzlePointTransform,
        float runAcceleration,
        float runSpeed,
        float drag,
        float gravityValue,
        float rotationSpeed,
        float directionChangeDelay,
        float turningDegrees)
    {
        PlayerTransform = playerTransform;
        CharacterController = characterController;
        PlayerCamera = playerCamera;
        PlayerLocomotionInput = playerLocomotionInput;
        MuzzlePointTransform = muzzlePointTransform;
        RunAcceleration = runAcceleration;
        RunSpeed = runSpeed;
        Drag = drag;
        GravityValue = gravityValue;
        RotationSpeed = rotationSpeed;
        DirectionChangeDelay = directionChangeDelay;
        TurningDegrees = turningDegrees;
    }
    
    // Helper methods for modifying runtime state
    public void SetVelocityY(float y)
    {
        PlayerVelocity = new Vector3(PlayerVelocity.x, y, PlayerVelocity.z);
    }
    
    public void AddVelocityY(float y)
    {
        PlayerVelocity = new Vector3(PlayerVelocity.x, PlayerVelocity.y + y, PlayerVelocity.z);
    }
    
}