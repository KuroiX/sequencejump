using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player
{
    // TODO: whole queue functionality really
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
            // TODO: "Action" input
            
            _inputManager.PlayerMovement.Move.performed += Move;
            _inputManager.PlayerMovement.Move.canceled += Move;
        }

        private void OnDisable()
        {
            TerminateInput();
        }

        private void TerminateInput()
        {
            // TODO: "Action" input
            
            _inputManager.PlayerMovement.Move.performed -= Move;
            _inputManager.PlayerMovement.Move.canceled -= Move;
        }

        private void Move(InputAction.CallbackContext ctx)
        {
            Horizontal = ctx.ReadValue<float>();
        }

        private void Action(InputAction.CallbackContext ctx)
        {
            // TODO: save currentAction in case of jump for short-hop
            
            Action currentAction = _actionQueue.Dequeue();

            switch (currentAction)
            {
                case Player.Action.Jump:
                    Jump(ctx);
                    break;
                case Player.Action.Dash:
                    Dash(ctx);
                    break;
                default:
                    throw new NotImplementedException(
                        "An Action was dequeued that has not been implemented in QueueProcessor.cs");
            }
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

        public float Horizontal { get; private set; }
        public bool JumpPerformed { get; private set; }
        public bool JumpBuffered => Time.unscaledTime - JumpTimeStamp < 0.1f;

        public bool JumpCanceled { get; private set; }
        public float JumpTimeStamp { get; set; }
        public float JumpEndTimeStamp { get; private set; }
        
        public bool DashPerformed { get; private set; }
    }
}