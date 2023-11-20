using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.Controller.CharacterInput
{
    public class StandardInputBehaviour : MonoBehaviour, IControllerInput, IInputHolder
    {
        public IControllerInput Input => this;
        
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
            _inputManager.PlayerMovement.Jump.performed += Jump;
            _inputManager.PlayerMovement.Jump.canceled += JumpEnd;
            _inputManager.PlayerMovement.Dash.performed += Dash;
            
            _inputManager.PlayerMovement.Move.performed += Move;
            _inputManager.PlayerMovement.Move.canceled += Move;
        }

        private void OnDisable()
        {
            TerminateInput();
        }

        private void TerminateInput()
        {
            _inputManager.PlayerMovement.Jump.performed -= Jump;
            _inputManager.PlayerMovement.Jump.canceled -= JumpEnd;
            _inputManager.PlayerMovement.Dash.performed -= Dash;
            
            _inputManager.PlayerMovement.Move.performed -= Move;
            _inputManager.PlayerMovement.Move.canceled -= Move;
        }

        private void Move(InputAction.CallbackContext ctx)
        {
            Horizontal = ctx.ReadValue<float>();
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            JumpPerformed = true;
            JumpTimeStamp = Time.unscaledTime;
        }

        private void JumpEnd(InputAction.CallbackContext ctx)
        {
            JumpCanceled = true;
            JumpEndTimeStamp = Time.unscaledTime;
        }

        private void Dash(InputAction.CallbackContext ctx)
        {
            DashPerformed = true;
        }
        
        private void LateUpdate()
        {
            JumpPerformed = false;
            JumpCanceled = false;
            DashPerformed = false;
        }

        public float Horizontal { get; set; }
        public bool JumpPerformed { get; set; }
        public bool JumpBuffered => Time.unscaledTime - JumpTimeStamp < 0.1f;

        public bool JumpCanceled { get; set; }
        public float JumpTimeStamp { get; set; }
        public float JumpEndTimeStamp { get; private set; }
        
        public bool DashPerformed { get; set; }
        public bool AirJumpPerformed { get; }
        public bool AirJumpCanceled { get; }
        public bool PlatformPerformed { get; }
        public bool PlatformCanceled { get; }
        public bool BaseJumpPerformed { get; }
        public bool BaseJumpCanceled { get; }
    }
}