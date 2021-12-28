using System;
using Features.Actions;
using Features.Queue;
using Features.Station;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.Controller
{
    // TODO: whole queue functionality really
    public class QueueProcessor : MonoBehaviour, ICharacterInput
    {
        private ResettableQueue<ICharacterAction> _actionQueue;
        
        private InputManager _inputManager;

        private void DisableInput(object sender, EventArgs args)
        {
            _inputManager.PlayerMovement.Disable();
        }
        
        private void EnableInput(object sender, EventArgs args)
        {
            _inputManager.PlayerMovement.Enable();
        }
        
        private void Awake()
        {
            _actionQueue = GetComponent<QueueHolder>().Queue;
            
            _inputManager = new InputManager();
            _inputManager.PlayerMovement.Enable();

        }
        
        private void OnEnable()
        {
            InitializeInput();
            
            Station.Station.StationOpened += DisableInput;
            Station.Station.StationClosed += EnableInput;
            
            HazardTriggerEnter.DeathAnimationStart += DisableInput;
            HazardTriggerEnter.DeathAnimationEnd += EnableInput;
        }

        private void InitializeInput()
        {
            // TODO: "Action" input
            _inputManager.PlayerMovement.Jump.performed += Action;
            _inputManager.PlayerMovement.Jump.canceled += JumpEnd;
            
            _inputManager.PlayerMovement.Move.performed += Move;
            _inputManager.PlayerMovement.Move.canceled += Move;
        }

        private void OnDisable()
        {
            TerminateInput();
            
            Station.Station.StationOpened -= DisableInput;
            Station.Station.StationClosed -= EnableInput;
            
            HazardTriggerEnter.DeathAnimationStart -= DisableInput;
            HazardTriggerEnter.DeathAnimationEnd -= EnableInput;
        }

        private void TerminateInput()
        {
            // TODO: "Action" input
            _inputManager.PlayerMovement.Jump.performed -= Action;
            _inputManager.PlayerMovement.Jump.canceled -= JumpEnd;
            
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
            if (_actionQueue.Count == 0) return;
            
            ICharacterAction currentAction = _actionQueue.Dequeue();

            switch (currentAction.Name)
            {
                case "Jump":
                    Jump(ctx);
                    break;
                case "Dash":
                    Dash(ctx);
                    break;
                default:
                    throw new NotImplementedException(
                        "An Action was dequeued that has not been implemented in QueueProcessor.cs");
            }
        }

        private bool _lastActionWasJump;

        private void Jump(InputAction.CallbackContext ctx)
        {
            JumpPerformed = true;
            JumpTimeStamp = Time.unscaledTime;
            _lastActionWasJump = true;
        }
        
        private void JumpEnd(InputAction.CallbackContext ctx)
        {
            if (!_lastActionWasJump) return;
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
