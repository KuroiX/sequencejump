using Features.Player.Controller.CharacterInput;
using UnityEngine;

namespace Features.Player.Controller.ControllerParts
{
    public class MovementController
    {
        private readonly IMovementInput _movementInput;
        private readonly GroundedController _grounded;
        private readonly Rigidbody2D _rb;
        private readonly MovementSettings _movementSettings;
        private float _scale;

        private readonly Ref<float> _maxFallSpeed;

        public MovementController(IMovementInput movementInput, GroundedController grounded, Rigidbody2D rb, MovementSettings movementSettings, Ref<float> maxFallSpeed)
        {
            _movementInput = movementInput;
            _grounded = grounded;
            _rb = rb;
            _scale = _rb.gravityScale;
            _movementSettings = movementSettings;
            _maxFallSpeed = maxFallSpeed;
        }

        public void HandleFixedUpdate()
        {
            float acceleration = _grounded.IsGrounded ? _movementSettings.groundAcceleration : _movementSettings.airAcceleration;
            float speed = _grounded.IsGrounded ? _movementSettings.groundSpeed : _movementSettings.airSpeed;

            Vector2 velocity = _rb.velocity;

            float targetSpeed = _movementInput.Horizontal * speed;

            float currentSpeed = velocity.x;

            // Potentially multiplication of acceleration with Time.fixedDeltaTime missing,
            // but shouldn't matter here since fixedDeltaTime should always be the same
            float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration);
            
            /*
            if (!_isGrounded)
            {
                _actualVelocity += Physics2D.gravity.y * Time.fixedDeltaTime * 5;
            }

            //float newSpeedY = velocity.y + (velocity.y <= 1 ? fallSpeedMultiplyer * Physics2D.gravity.y * Time.fixedDeltaTime : 0);

            float newSpeedY = newSpeedY < -maxFallSpeed ? -maxFallSpeed : newSpeedY;

            float newSpeedY = 0;

            if (_actualVelocity > 2)
            {
                newSpeedY = jumpForce;
            }
            else 
            {
                newSpeedY = _actualVelocity * .7f;
            }
            */

            float newSpeedY = velocity.y;
            float progress = 1;

            if (newSpeedY < -1)
            {
                progress -= newSpeedY / -(_maxFallSpeed.Value*2);
            }

            _rb.gravityScale =  _scale * progress;
            
            if (newSpeedY < -_maxFallSpeed.Value)
            {
                Debug.Log("reached max fall speed");
                //Debug.Break();
            }
            newSpeedY = newSpeedY < -_maxFallSpeed.Value ? -_maxFallSpeed.Value : newSpeedY;
            
            _rb.velocity = new Vector2(newSpeed, newSpeedY);
        }
    }
}