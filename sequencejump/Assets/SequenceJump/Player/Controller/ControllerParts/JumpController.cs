using UnityEngine;

namespace SequenceJump.Player.Controller.ControllerParts
{
    public class JumpController
    {
        private readonly Rigidbody2D _rb;
        private readonly float _jumpHeight;
        
        public bool IsJumping { get; private set; }
        
        public JumpController(Rigidbody2D rb, float jumpHeight)
        {
            _rb = rb;
            _jumpHeight = jumpHeight;
        }
        
        public void Jump()
        {
            //Debug.Log("Jump");
            _rb.velocity = new Vector2(_rb.velocity.x, CalculateJumpVelocity(_jumpHeight));
            IsJumping = true;
        }

        public void JumpEnd(bool shortHop)
        {
            Vector2 velocity = _rb.velocity;
            
            if (shortHop && IsJumping && velocity.y > 0)
            {
                _rb.velocity = new Vector2(velocity.x, velocity.y * 0.5f);
            }
            IsJumping = false;
        }
        
        private float CalculateJumpVelocity(float jumpHeight)
        {
            // TODO: Should be moved to Constructor() later when we decided on a jumpHeight
            
            // Basically explicit euler
            float fixedTimeStep = Time.fixedDeltaTime;
            float gravity = Physics2D.gravity.y * _rb.gravityScale;

            float position = 0;
            float velocity = 0;

            while (true)
            {
                position += velocity * fixedTimeStep;
                velocity += gravity * fixedTimeStep;
                
                if (position < -jumpHeight)
                {
                    break;
                }
            }

            return -velocity;
        }
    }
}