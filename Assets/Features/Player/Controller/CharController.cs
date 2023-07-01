using Features.Player.Controller.CharacterInput;
using Features.Player.Controller.ControllerParts;

namespace Features.Player.Controller
{
    public class CharController
    {
        private readonly GroundedController _grounded;
        private readonly JumpController _jump;
        private readonly JumpController _airJump;
        private readonly IDashController _dash;
        private readonly MovementController _movement;
        private readonly PlatformController _platform;

        private readonly IControllerInput _controllerInput;
        private float _direction;
        
        public float Direction => _direction;

        public CharController(GroundedController grounded, JumpController jump, JumpController airJump, 
            IDashController dash, MovementController movement, PlatformController platform,
            IControllerInput controllerInput)
        {
            _grounded = grounded;
            _jump = jump;
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