using System;
using UnityEngine.InputSystem;

namespace Features.Player
{
    public class InputManagerInput : ICharacterInput
    {
        public event Action<InputAction.CallbackContext> ActionPerformed
        {
            add => _inputManager.PlayerMovement.Jump.performed += value;
            remove => _inputManager.PlayerMovement.Jump.performed -= value;
        }

        public event Action<InputAction.CallbackContext> ActionCanceled
        {
            add => _inputManager.PlayerMovement.Jump.canceled += value;
            remove => _inputManager.PlayerMovement.Jump.canceled -= value;
        }
        
        public event Action<InputAction.CallbackContext> MovePerformed
        {
            add => _inputManager.PlayerMovement.Move.performed += value;
            remove => _inputManager.PlayerMovement.Move.performed -= value;
        }
        
        public event Action<InputAction.CallbackContext> MoveCanceled
        {
            add => _inputManager.PlayerMovement.Move.canceled += value;
            remove => _inputManager.PlayerMovement.Move.canceled -= value;
        }

        private readonly InputManager _inputManager;

        public InputManagerInput(InputManager inputManager)
        {
            _inputManager = inputManager;
        }
    }
}