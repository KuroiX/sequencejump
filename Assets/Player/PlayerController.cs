using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private InputManager _inputManager;

        private Rigidbody2D _rb;
        private BoxCollider2D _collider;

        [System.Serializable]
        private struct MovementSettings
        {
            [Header("Ground Settings")]
            public float groundSpeed;
            public float groundAcceleration;
            [Tooltip("Copy from ground options if no other behavior is wanted.")]
            [Header("Air Settings")]
            public float airSpeed;
            public float airAcceleration;
        }

        [SerializeField] private MovementSettings movementSettings;

        [Header("Other Settings")]
        [SerializeField] private float jumpHeight;
        [SerializeField] private float maxFallSpeed;
        [SerializeField] private bool shortHoppable;
        [SerializeField] private LayerMask environmentLayerMask;

        #region Input-Handling
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            //_cc = GetComponent<CharacterController>();
            _collider = GetComponent<BoxCollider2D>();
        
            _inputManager = new InputManager();
            _inputManager.PlayerMovement.Enable();
        }

        private void OnEnable()
        {
            InitializeInput();
        }

        private void OnDisable()
        {
            TerminateInput();
        }

        private void InitializeInput()
        {
            _inputManager.PlayerMovement.Jump.performed += Jump;
            _inputManager.PlayerMovement.Jump.canceled += JumpEnd;
            _inputManager.PlayerMovement.Dash.performed += Dash;
        
            _inputManager.PlayerMovement.Move.performed += Move;
            _inputManager.PlayerMovement.Move.canceled += Move;
        }

        private void TerminateInput()
        {
            _inputManager.PlayerMovement.Jump.performed -= Jump;
            _inputManager.PlayerMovement.Dash.performed -= Dash;
        
            _inputManager.PlayerMovement.Move.performed -= Move;
            _inputManager.PlayerMovement.Move.canceled -= Move;
        }
        
        #endregion

        private bool _isGrounded;

        private bool _isJumping = false;

        private float CalculateJumpVelocity(float jumpHeight)
        {
            // Should be moved to Start() later
            
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

        private void Jump(InputAction.CallbackContext ctx)
        {
            Debug.Log("Jump");
            if (_isGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, CalculateJumpVelocity(jumpHeight));
                _isJumping = true;
            }
        }

        private void JumpEnd(InputAction.CallbackContext ctx)
        {
            Vector2 velocity = _rb.velocity;
            
            if (shortHoppable && _isJumping && velocity.y > 0)
            {
                _rb.velocity = new Vector2(velocity.x, velocity.y * 0.5f);
                _isJumping = false;
            }
        }
    
        private void Dash(InputAction.CallbackContext ctx)
        {
            Debug.Log("Dash");
        }

        private float _movementInput;
    
        private void Move(InputAction.CallbackContext ctx)
        {
            _movementInput = ctx.ReadValue<float>();
        }

        private float _actualVelocity = 0;

        private void FixedUpdate()
        {
            #region x
            float acceleration = _isGrounded ? movementSettings.groundAcceleration : movementSettings.airAcceleration;
            float speed = _isGrounded ? movementSettings.groundSpeed : movementSettings.airSpeed;

            Vector2 velocity = _rb.velocity;

            float targetSpeed = _movementInput * speed;

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

            DrawBoxDebug(result, bounds, xMargin, yMargin);

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

        // TODO: FixedUpdate?
        private void Update()
        {
            bool newGrounded = IsGrounded();
            
            if (!_cliffJumpBufferRoutineStarted && _isGrounded && !newGrounded)
            {
                Debug.Log("Bois");
                _cliffJumpBuffer = StartCoroutine(CliffJumpBuffer());
                _cliffJumpBufferRoutineStarted = true;
            }
            else if (newGrounded)
            {
                _isGrounded = true;
                _cliffJumpBufferRoutineStarted = false;
                if (_cliffJumpBuffer != null)
                {
                    StopCoroutine(_cliffJumpBuffer);
                }
            }
        }

        private Coroutine _cliffJumpBuffer;
        private bool _cliffJumpBufferRoutineStarted;

        private IEnumerator CliffJumpBuffer()
        {
            yield return new WaitForSeconds(.1f);
            //Jump(new InputAction.CallbackContext());
            _isGrounded = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Entered trigger");
        }
    }
}
