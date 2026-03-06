using System;
using NUnit.Framework;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState
    {
        Locomotion,
        Aiming,
        Attacking
    }
    
    private PlayerContext _context;
    
    [Header("Components")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;
    private PlayerLocomotionInput _playerLocomotionInput;
   
   
    [Header("Base Movement Settings")]
    [SerializeField] private float runAcceleration;
    [SerializeField] private float runSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float gravityValue;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float directionChangeDelay;
    [SerializeField] private float turningDegrees; // Threshold for considering a direction change as a "turn"

    // gravity variables
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
   
    // direction change variables
    private Vector3 _previousMovementDirection;
    private float _directionChangeTimer;

    private void Awake()
    {
        ValidateVariables();
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        _context = new PlayerContext(
            transform,
            _characterController,
            _playerCamera,
            _playerLocomotionInput,
            runAcceleration,
            runSpeed,
            drag,
            gravityValue,
            rotationSpeed,
            directionChangeDelay,
            turningDegrees);

        InitializeStates();
    }

    private void ValidateVariables()
    {
        Assert.IsNotNull(_characterController, "CharacterController is not assigned in the inspector.");
        Assert.IsNotNull(_playerCamera, "Player Camera is not assigned in the inspector.");
        Assert.Greater(runAcceleration, 0f, "runAcceleration must be greater than 0.");
        Assert.Greater(runSpeed, 0f, "runSpeed must be greater than 0.");
        Assert.Greater(drag, 0f, "drag must be greater than 0.");
        Assert.Less(gravityValue, 0f, "gravityValue must be greater than 0.");
        Assert.Greater(rotationSpeed, 0f, "rotationSpeed must be greater than 0.");
        Assert.Greater(directionChangeDelay, 0f, "directionChangeDelay must be greater than 0.");
        Assert.Greater(turningDegrees, 0f, "turningDegrees must be greater than 0.");
    }

    private void InitializeStates()
    {
        States.Add(EPlayerState.Locomotion, new PlayerLocomotionState(_context, EPlayerState.Locomotion));
        States.Add(EPlayerState.Aiming, new PlayerAimingState(_context, EPlayerState.Aiming));
        States.Add(EPlayerState.Attacking, new PlayerAttackState(_context, EPlayerState.Attacking));
        CurrentState = States[EPlayerState.Locomotion];
    }
}
