using UnityEngine;

namespace Features.Player
{
    public class DashController
    {
        private readonly float _initialGravityScale;
        private float _dashTime;

        private readonly int _iterations;
        private readonly float _dashDistance;

        private readonly Rigidbody2D _rb;
        
        public bool IsDashing { get; private set; }

        public DashController(Rigidbody2D rb, int iterations, float dashDistance)
        {
            _rb = rb;
            _initialGravityScale = rb.gravityScale;

            _iterations = iterations;
            _dashDistance = dashDistance;
        }

        public void Dash(float direction)
        {
            //Debug.Log("Dash in Controller");
            _dashTime = _iterations * Time.fixedDeltaTime;
            float actualSpeed = _dashDistance / _dashTime;
            
            _rb.velocity = new Vector2(actualSpeed * direction, 0);
            IsDashing = true;
            _rb.gravityScale = 0;
        }

        public void DashEnd()
        {
            _rb.gravityScale = _initialGravityScale;
            _rb.velocity = new Vector2(0, 0);
            IsDashing = false;
        }

        public void HandleFixedUpdate()
        {
            _dashTime -= Time.fixedDeltaTime;
            if (_dashTime < 0 && IsDashing) DashEnd();
        }
    }
}