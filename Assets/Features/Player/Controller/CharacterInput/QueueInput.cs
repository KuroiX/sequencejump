using UnityEngine.InputSystem;

namespace Features.Player.Controller.CharacterInput
{
    public class QueueInput
    {
        private readonly QueueProcessor _queueProcessor;
        private readonly InputManager _inputManager;

        private readonly ICharacterInput _characterInput;
        
        public QueueInput(QueueProcessor queueProcessor, InputManager inputManager, ICharacterInput characterInput)
        {
            _inputManager = inputManager;
            _characterInput = characterInput;
            _queueProcessor = queueProcessor;
        }
        
        public void HandleOnEnable()
        {
            _inputManager.PlayerMovement.Jump.performed += Action;
            _inputManager.PlayerMovement.Jump.canceled += ActionEnd;
            
            _inputManager.PlayerMovement.Move.performed += Move;
            _inputManager.PlayerMovement.Move.canceled += Move;
        }
        
        public void HandleOnDisable()
        {
            _inputManager.PlayerMovement.Jump.performed -= Action;
            _inputManager.PlayerMovement.Jump.canceled -= ActionEnd;
            
            _inputManager.PlayerMovement.Move.performed -= Move;
            _inputManager.PlayerMovement.Move.canceled -= Move;
        }
        
        private void Move(InputAction.CallbackContext ctx)
        {
            _characterInput.Horizontal = ctx.ReadValue<float>();
        }

        private void Action(InputAction.CallbackContext ctx)
        {
            _queueProcessor.Action(_characterInput);
        }

        private void ActionEnd(InputAction.CallbackContext ctx)
        {
            _queueProcessor.JumpEnd(_characterInput);
        }

        public void HandleLateUpdate()
        {
            _characterInput.JumpPerformed = false;
            _characterInput.JumpCanceled = false;
            _characterInput.DashPerformed = false;
        }
    }
}