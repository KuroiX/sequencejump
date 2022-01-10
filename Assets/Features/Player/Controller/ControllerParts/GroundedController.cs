using UnityEngine;

namespace Features.Player.Controller.ControllerParts
{
    public class GroundedController
    {
        private readonly Collider2D _collider;

        private bool _isGrounded;
        public bool IsGrounded => _isGrounded;

        private float _coyoteTimeStamp;
        public float CoyoteTimeStamp => _coyoteTimeStamp;
        public bool IsCoyoteGrounded => _isGrounded || (Time.unscaledTime - _coyoteTimeStamp) < _coyoteTimeFrame;

        private readonly LayerMask _groundLayerMask;
        private readonly float _coyoteTimeFrame;
        
        public GroundedController(Collider2D collider, LayerMask groundLayerMask, float coyoteTimeFrame)
        {
            _collider = collider;
            _groundLayerMask = groundLayerMask;
            _coyoteTimeFrame = coyoteTimeFrame;
        }

        public void HandleUpdate()
        {
            bool wasGrounded = _isGrounded;
            SetGrounded();
            SetCoyote(wasGrounded, _isGrounded);
        }
        
        private void SetCoyote(bool wasGrounded, bool isGrounded)
        {
            if (wasGrounded && !isGrounded)
            {
                _coyoteTimeStamp = Time.unscaledTime;
            }
        }
        
        private void SetGrounded()
        {
            Bounds bounds = _collider.bounds;
            float yMargin = 0.1f;
            float xMargin = 0.05f;
        
            Collider2D col = Physics2D.OverlapBox(
                (Vector2)bounds.center + Vector2.down * bounds.extents.y,
                new Vector2(bounds.extents.x - xMargin, yMargin) * 2, 
                0f, 
                _groundLayerMask);

            bool result = !ReferenceEquals(col, null); // col != null;
            
#if UNITY_EDITOR
            DrawBoxDebug(result, bounds, xMargin, yMargin);
#endif

            _isGrounded = result;
        }

#if UNITY_EDITOR
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
#endif
    }
}