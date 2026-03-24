using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)] // Ensure this script runs before other scripts that might depend on input data
public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
{
        public PlayerControls PlayerControls { get; private set; }
        public Vector2 MovementInput { get; private set; } // handle WASD or left stick input as a Vector2. Handle float values for smooth movement instead of just on/off. X = horizontal input, Y = vertical input.
        public bool AimInput { get; private set; } // true or false based on whether the button is pressed or not
        public bool AttackInput { get; private set; }

        private void OnEnable()
        {
                PlayerControls = new PlayerControls(); // Create input system instance
                PlayerControls.Enable(); // Enable all action maps
                
                PlayerControls.PlayerLocomotionMap.Enable(); // Enable specific map
                PlayerControls.PlayerLocomotionMap.SetCallbacks(this); // Register THIS script as the callback receiver
                // Whenever something happens in PlayerLocomotionMap, call my methods (OnMovement, OnAim, etc.)
        }

        private void OnDisable()
        {
                PlayerControls.PlayerLocomotionMap.Disable();
                PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this); // Clean up callbacks
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
                MovementInput = context.ReadValue<Vector2>();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
                AimInput = context.ReadValueAsButton();
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
                AttackInput = context.ReadValueAsButton();
        }
}
