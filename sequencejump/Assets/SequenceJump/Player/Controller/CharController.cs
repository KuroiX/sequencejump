﻿using SequenceJump.Player.Controller.CharacterInput;
using SequenceJump.Player.Controller.ControllerParts;

namespace SequenceJump.Player.Controller
{
    public class CharController
    {
        private readonly GroundedController _grounded;
        private readonly JumpController _jump;
        private readonly JumpController _baseJump;
        private readonly JumpController _airJump;
        private readonly IDashController _dash;
        private readonly MovementController _movement;
        private readonly PlatformController _platform;

        private readonly IControllerInput _controllerInput;
        private float _direction;
        
        public float Direction => _direction;

        public CharController(GroundedController grounded, JumpController jump, JumpController baseJump, JumpController airJump, 
            IDashController dash, MovementController movement, PlatformController platform,
            IControllerInput controllerInput)
        {
            _grounded = grounded;
            _jump = jump;
            _baseJump = baseJump;
            _airJump = airJump;
            _dash = dash;
            _movement = movement;
            _controllerInput = controllerInput;
            _platform = platform;
        }

        public void HandleUpdate()
        {
            _grounded.HandleUpdate();
            CalculateDirection();

            HandleJump();
            HandleDash();
            HandleAirJump();
            HandlePlatform();
            HandleBaseJump();
        }

        public void HandleFixedUpdate()
        {
            _dash.HandleFixedUpdate();
            
            if (!_dash.IsDashing) _movement.HandleFixedUpdate();
        }
        
        private bool IsAllowedToJump()
        {
            return _grounded.IsCoyoteGrounded && !_jump.IsJumping;
        }

        private void HandleJump()
        {
            if ((_controllerInput.JumpPerformed) &&
                IsAllowedToJump())
            {
                _dash.DashEnd();
                _jump.Jump();
            }
            
            //if (_characterInput.JumpCanceled) _jump.JumpEnd(shortHoppable);
            if (_controllerInput.JumpCanceled) _jump.JumpEnd(false);
        }

        private void HandleAirJump()
        {
            if (_controllerInput.AirJumpPerformed)
            {
                _dash.DashEnd();
                _airJump.Jump();
            } 
            else if (_controllerInput.AirJumpCanceled)
            {
                _airJump.JumpEnd(false);
            }
        }

        private void HandleBaseJump()
        {
            if ((_controllerInput.BaseJumpPerformed) &&
                IsAllowedToJump())
            {
                _dash.DashEnd();
                _baseJump.Jump();
            }
            
            //if (_characterInput.JumpCanceled) _jump.JumpEnd(shortHoppable);
            if (_controllerInput.BaseJumpCanceled) _baseJump.JumpEnd(true);
        }

        private void HandleDash()
        {
            if (_controllerInput.DashPerformed) _dash.Dash(_direction);
        }

        private void HandlePlatform()
        {
            if (!_controllerInput.PlatformPerformed) return;

            _platform.Trigger();
        }
        
        private void CalculateDirection()
        {
            _direction = _controllerInput.Horizontal != 0 ? _controllerInput.Horizontal : _direction;
        }
    }
}