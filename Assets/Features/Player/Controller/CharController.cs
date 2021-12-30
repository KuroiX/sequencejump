using Features.Player.Controller.CharacterInput;
using Features.Player.Controller.ControllerParts;

namespace Features.Player.Controller
{
    public class CharController
    {
        private readonly GroundedController _grounded;
        private readonly JumpController _jump;
        private readonly DashController _dash;
        private readonly MovementController _movement;

        private readonly IControllerInput _controllerInput;
        private float _direction;
        
        public float Direction => _direction;

        public CharController(GroundedController grounded, JumpController jump, DashController dash, MovementController movement, IControllerInput controllerInput)
        {
            _grounded = grounded;
            _jump = jump;
            _dash = dash;
            _movement = movement;
            _controllerInput = controllerInput;
        }

        public void HandleUpdate()
        {
            _grounded.HandleUpdate();
            CalculateDirection();

            HandleJump();
            HandleDash();
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

        private void HandleDash()
        {
            if (_controllerInput.DashPerformed) _dash.Dash(_direction);
        }
        
        private void CalculateDirection()
        {
            _direction = _controllerInput.Horizontal != 0 ? _controllerInput.Horizontal : _direction;
        }
    }
}