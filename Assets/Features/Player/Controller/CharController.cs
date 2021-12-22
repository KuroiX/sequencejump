using Features.Player.Controller.ControllerParts;

namespace Features.Player.Controller
{
    public class CharController
    {
        private GroundedController _grounded;
        private JumpController _jump;
        private DashController _dash;
        private MovementController _movement;

        private ICharacterInput _characterInput;
        
        
        public void HandleUpdate()
        {
            _grounded.HandleUpdate();
            CalculateDirection();
            FlipDirection();

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
            if ((_characterInput.JumpPerformed || _characterInput.JumpBuffered) &&
                IsAllowedToJump())
            {
                _dash.DashEnd();
                _jump.Jump();
            }
            
            if (_characterInput.JumpCanceled) _jump.JumpEnd(shortHoppable);
        }

        private void HandleDash()
        {
            if (_characterInput.DashPerformed) _dash.Dash(_direction);
        }
    }
}