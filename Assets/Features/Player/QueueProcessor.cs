using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player
{
    public class QueueProcessor : MonoBehaviour, ICharacterInput
    {
        private ResettableQueue<Action> _actionQueue = new ResettableQueue<Action>();
        
        private InputManager _inputManager;

        private void Awake()
        {
            _inputManager = new InputManager();
            _inputManager.PlayerMovement.Enable();
        }
        
        private void OnEnable()
        {
            InitializeInput();
        }

        private void InitializeInput()
        {
            _inputManager.PlayerMovement.Jump.performed += Jump2;
            _inputManager.PlayerMovement.Jump.canceled += JumpEnd;
            _inputManager.PlayerMovement.Dash.performed += Dash2;
            
            _inputManager.PlayerMovement.Move.performed += Move;
            _inputManager.PlayerMovement.Move.canceled += Move;
        }

        private void OnDisable()
        {
            TerminateInput();
        }

        private void TerminateInput()
        {
            _inputManager.PlayerMovement.Move.performed -= Move;
            _inputManager.PlayerMovement.Move.canceled -= Move;
        }

        private void Move(InputAction.CallbackContext ctx)
        {
            Horizontal = ctx.ReadValue<float>();
        }

        private void Jump2(InputAction.CallbackContext ctx)
        {
            Jump = true;
        }

        private void JumpEnd(InputAction.CallbackContext ctx)
        {
            Jump = false;
        }

        private void Dash2(InputAction.CallbackContext ctx)
        {
            Dash = true;
        }

        public float Horizontal { get; private set; }
        public bool Jump { get; private set; }
        public bool Dash { get; private set; }
    }
}
