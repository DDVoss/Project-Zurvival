using System;
using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
   [Header("Components")]
   [SerializeField] private CharacterController _characterController;
   [SerializeField] private Camera _playerCamera;
   
   
   [Header("Base Movement Settings")]
   public float runAcceleration = 0.25f;
   public float runSpeed = 4f;
   public float drag = 0.1f;
   public float gravityValue = -9.81f;
   public float rotationSpeed = 10f;
   public float directionChangeDelay = 0.3f;
   public float turningDegrees = 90f; // Threshold for considering a direction change as a "turn"

   // gravity variables
   private Vector3 _playerVelocity;
   private bool _groundedPlayer;
   
   // direction change variables
   private Vector3 _previousMovementDirection;
   private float _directionChangeTimer;

   private PlayerLocomotionInput _playerLocomotionInput;

   
   private void Awake()
   {
      _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
   }

   // private void Update()
   // {
   //    
   //    _groundedPlayer = _characterController.isGrounded;
   //    if (_groundedPlayer)
   //    {
   //       if(_playerVelocity.y < -2f)
   //       {
   //          _playerVelocity.y = -2f;
   //       }
   //    }
   //    
   //    Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
   //    Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
   //    Vector3 movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x +
   //                                cameraForwardXZ * _playerLocomotionInput.MovementInput.y;
   //    
   //    
   //    // Rotate the player to face the movement direction
   //    if (movementDirection.magnitude > 0.1f)
   //    {
   //       Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
   //       transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
   //    }
   //
   //    
   //    Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime;
   //    Vector3 newVelocity = _characterController.velocity + movementDelta;
   //    
   //    // Apply drag to player
   //    Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
   //    newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
   //    newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);
   //    
   //    // Apply gravity to player
   //    _playerVelocity.y += gravityValue * Time.deltaTime;
   //    
   //    // Combine horizontal and vertical movement
   //    Vector3 totalMovement = newVelocity * Time.deltaTime;
   //    totalMovement.y = _playerVelocity.y * Time.deltaTime;
   //    
   //    // Move character (Unity suggests only calling this once per tick)
   //    _characterController.Move(totalMovement);
   //    
   // }
   
   
   // Testing claude suggestion on direction change delay to smooth out movement when changing directions quickly
private void Update()
   {
       _groundedPlayer = _characterController.isGrounded;
       if (_groundedPlayer)
       {
           if(_playerVelocity.y < -2f)
           {
               _playerVelocity.y = -2f;
           }
       }
   
       Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
       Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
       Vector3 movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x +
                                   cameraForwardXZ * _playerLocomotionInput.MovementInput.y;
   
       // Check if direction changed significantly
       if (movementDirection.magnitude > 0.1f)
       {
           float directionDifference = Vector3.Angle(_previousMovementDirection, movementDirection);
           
           if (directionDifference > turningDegrees) // If turning more than 90 degrees
           {
               _directionChangeTimer = directionChangeDelay;
           }
           _previousMovementDirection = movementDirection;
           
           // Rotate the player to face the movement direction
           if (movementDirection.magnitude > 0.1f)
           {
              Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
              transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
           }

       }
   
       // Count down the timer
       if (_directionChangeTimer > 0f)
       {
           _directionChangeTimer -= Time.deltaTime;
       }
   
       // Only apply movement if timer is zero or player is moving in same direction
       float movementMultiplier = _directionChangeTimer > 0f ? 0f : 1f;
       
       Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime * movementMultiplier;
       Vector3 newVelocity = _characterController.velocity + movementDelta;
   
       // Apply drag to player
       Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
       newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
       newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);
   
       // Apply gravity to player
       _playerVelocity.y += gravityValue * Time.deltaTime;
   
       // Combine horizontal and vertical movement
       Vector3 totalMovement = newVelocity * Time.deltaTime;
       totalMovement.y = _playerVelocity.y * Time.deltaTime;
   
       // Move character
       _characterController.Move(totalMovement);
   }
   

}
