using UnityEngine;

namespace Features.Player
{
    public class CharacterControllerBehaviour : MonoBehaviour
    {
        private ICharacterInput _characterInput;
        private Rigidbody2D _rb;
        private BoxCollider2D _collider;

        [SerializeField] private MovementSettings movementSettings;
        
        [Header("Jump Settings")]
        [SerializeField] private float jumpHeight;
        [SerializeField] private bool shortHoppable;
        
        [Header("Dash Settings")]
        [SerializeField] private float dashDistance;
        [SerializeField] private int iterations;

        [Header("Other Settings")]
        [SerializeField] private float maxFallSpeed;
        [SerializeField] private LayerMask environmentLayerMask;
        [SerializeField] private bool useStandardInput;
        
        private bool _isGrounded;

        private float _coyoteTimeStamp;
        private float _direction;

        private JumpController _jump;
        private DashController _dash;

        #region MonoBehaviour
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            int i = useStandardInput ? 0 : 1;
            _characterInput = GetComponents<ICharacterInput>()[i];

            _jump = new JumpController(_rb, jumpHeight);
            _dash = new DashController(_rb, iterations, dashDistance);

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            _dash.HandleFixedUpdate();
            
            if (_dash.IsDashing) return;
            
            #region x
            float acceleration = _isGrounded ? movementSettings.groundAcceleration : movementSettings.airAcceleration;
            float speed = _isGrounded ? movementSettings.groundSpeed : movementSettings.airSpeed;

            Vector2 velocity = _rb.velocity;

            float targetSpeed = _characterInput.Horizontal * speed;

            float currentSpeed = velocity.x;

            // Potentially multiplication of acceleration with Time.fixedDeltaTime missing,
            // but shouldn't matter here since fixedDeltaTime should always be the same
            float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration);
            
            #endregion

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
            newSpeedY = newSpeedY < -maxFallSpeed ? -maxFallSpeed : newSpeedY;
            
            _rb.velocity = new Vector2(newSpeed, newSpeedY);

        }
        
        private void Update()
        {
            CheckGroundedAndCoyote();
            CalculateDirection();
            FlipDirection();

            if ((_characterInput.JumpPerformed || _characterInput.JumpBuffered) &&
                IsAllowedToJump())
            {
                _dash.DashEnd();
                _jump.Jump();
            }
            
            if (_characterInput.JumpCanceled) _jump.JumpEnd(shortHoppable);

            if (_characterInput.DashPerformed) _dash.Dash(_direction);
        }

        #endregion MonoBehaviour
        
        #region Gathering Information
        
        private void CheckGroundedAndCoyote()
        {
            bool wasGrounded = _isGrounded;
            _isGrounded = IsGrounded();
            
            if (wasGrounded && !_isGrounded && !_jump.IsJumping)
            {
                _coyoteTimeStamp = Time.unscaledTime;
            }
        }
        
        private bool IsGrounded()
        {
            Bounds bounds = _collider.bounds;
            float yMargin = 0.1f;
            float xMargin = 0.05f;
        
            Collider2D col = Physics2D.OverlapBox(
                (Vector2)bounds.center + Vector2.down * bounds.extents.y,
                new Vector2(bounds.extents.x - xMargin, yMargin) * 2, 
                0f, 
                environmentLayerMask);
        
            bool result = col != null;
            
#if UNITY_EDITOR
            DrawBoxDebug(result, bounds, xMargin, yMargin);
#endif

            return result;
        }

        private void DrawBoxDebug(bool result, Bounds bounds, float xMargin, float yMargin)
        {
            Color rayColor = result ? Color.green : Color.red;

            // Top-left to right
            Debug.DrawRay(bounds.min + Vector3.up * yMargin + Vector3.right * xMargin, Vector2.right * ((bounds.extents.x - xMargin) * 2), rayColor);
            // Top-left to down
            Debug.DrawRay(bounds.min + Vector3.up * yMargin + Vector3.right * xMargin, Vector2.down * (yMargin * 2), rayColor);
            // Bottom-left to right
            Debug.DrawRay(bounds.min + Vector3.down * yMargin + Vector3.right * xMargin, Vector2.right * ((bounds.extents.x - xMargin) * 2), rayColor);
            // Top-right to down
            Debug.DrawRay(bounds.min + Vector3.up * yMargin + Vector3.right * (bounds.extents.y * 2) - Vector3.right * xMargin, 
                Vector2.down * (yMargin * 2), rayColor);
        }

        private SpriteRenderer _spriteRenderer;

        private void CalculateDirection()
        {
            _direction = _characterInput.Horizontal != 0 ? _characterInput.Horizontal : _direction;
        }

        private void FlipDirection()
        {
            if (_direction < 0)
            {
                _spriteRenderer.flipX = true;
            } 
            else if (_direction > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
        
        private bool IsAllowedToJump()
        {
            bool canCoyoteJump = (Time.unscaledTime - _coyoteTimeStamp) < .1f;
            return _isGrounded || canCoyoteJump;
        }

        #endregion
    }
}
