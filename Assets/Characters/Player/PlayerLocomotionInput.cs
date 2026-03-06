using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
{
        public PlayerControls PlayerControls { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public bool AimInput { get; private set; }
        public bool AttackInput { get; private set; }

        private void OnEnable()
        {
                PlayerControls = new PlayerControls();
                PlayerControls.Enable();
                
                PlayerControls.PlayerLocomotionMap.Enable();
                PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
                PlayerControls.PlayerLocomotionMap.Disable();
                PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
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
