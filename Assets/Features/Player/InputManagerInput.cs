using System;
using System.Collections.Generic;
using NSubstitute.Core;
using UnityEngine.InputSystem;

namespace Features.Player
{
    public class InputManagerInput : ICharacterInput
    {
        public event Action ActionPerformed
        {
            add
            {
                Action<InputAction.CallbackContext> sub = ctx => DoAction(value);
                _actionPerformed.Add(value, sub);
                _inputManager.PlayerMovement.Jump.performed += sub;
            }
            remove
            {
                var unsub = _actionPerformed[value];
                _actionPerformed.Remove(value);
                _inputManager.PlayerMovement.Jump.performed -= unsub;
            }
        }

        public event Action ActionCanceled
        {
            add
            {
                Action<InputAction.CallbackContext> sub = ctx => DoAction(value);
                _actionCanceled.Add(value, sub);
                _inputManager.PlayerMovement.Jump.canceled += sub;
            }
            remove
            {
                var unsub = _actionCanceled[value];
                _actionCanceled.Remove(value);
                _inputManager.PlayerMovement.Jump.canceled -= unsub;
            }
        }

        public event Action<float> MovePerformed
        {
            add 
            {
                Action<InputAction.CallbackContext> sub = ctx => Move(value, ctx);
                _movePerformed.Add(value, sub);
                _inputManager.PlayerMovement.Move.performed += sub;
            }
            remove 
            {
                var unsub = _movePerformed[value];
                _movePerformed.Remove(value);
                _inputManager.PlayerMovement.Move.performed -= unsub;
            }
        }

        public event Action<float> MoveCanceled
        {
            add 
            {
                Action<InputAction.CallbackContext> sub = ctx => Move(value, ctx);
                _moveCanceled.Add(value, sub);
                _inputManager.PlayerMovement.Move.canceled += sub;
            }
            remove 
            {
                var unsub = _moveCanceled[value];
                _moveCanceled.Remove(value);
                _inputManager.PlayerMovement.Move.canceled -= unsub;
            }
        }

        private readonly Dictionary<Action, Action<InputAction.CallbackContext>> _actionPerformed =
            new Dictionary<Action, Action<InputAction.CallbackContext>>();

        private readonly Dictionary<Action, Action<InputAction.CallbackContext>> _actionCanceled =
            new Dictionary<Action, Action<InputAction.CallbackContext>>();
        
        private readonly Dictionary<Action<float>, Action<InputAction.CallbackContext>> _movePerformed =
            new Dictionary<Action<float>, Action<InputAction.CallbackContext>>();
        
        private readonly Dictionary<Action<float>, Action<InputAction.CallbackContext>> _moveCanceled =
            new Dictionary<Action<float>, Action<InputAction.CallbackContext>>();

        private readonly InputManager _inputManager;

        public InputManagerInput(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void DoAction(Action value)
        {
            value();
        }

        private void Move(Action<float> value, InputAction.CallbackContext ctx)
        {
            value(ctx.ReadValue<float>());
        }
    }
}