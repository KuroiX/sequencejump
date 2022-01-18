using Features.Player.Controller;
using UnityEngine;

namespace Features.Player
{
    public class DashController
    {
        private readonly float _initialGravityScale;
        private float _dashTime;
        private float _activeTime;
        private float _speed;
        private float _direction;

        private readonly Ref<int> _iterations;
        private readonly Ref<float> _dashDistance;
        private readonly Ref<float> _breakPoint;

        private float TimeBreakPoint
        {
            get
            {
                float doubler = _dashDistance.Value - _breakPoint.Value;
                doubler *= 2;

                float denom = doubler + _breakPoint.Value;
                return (_dashTime * _breakPoint.Value / denom);
            }
        }

        private readonly Rigidbody2D _rb;
        
        public bool IsDashing { get; private set; }

        public DashController(Rigidbody2D rb, Ref<int> iterations, Ref<float> dashDistance, Ref<float> breakPoint)
        {
            _rb = rb;
            _initialGravityScale = rb.gravityScale;

            _iterations = iterations;
            _dashDistance = dashDistance;
            _breakPoint = breakPoint;
        }

        public void Dash(float direction)
        {
            //Debug.Log("Dash in Controller");
            _dashTime = _iterations.Value * Time.fixedDeltaTime;
            _activeTime = 0;

            float actualSpeed = TimeBreakPoint == 0 ? CalculateA() * 2 * (-_dashTime) : _breakPoint.Value / TimeBreakPoint;
            //float actualSpeed = _dashDistance.Value / _dashTime;

            _speed = actualSpeed * direction;
            _direction = direction;
            
            _rb.velocity = new Vector2(_speed, 0);
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
            if (!IsDashing) return;
            
            _activeTime += Time.fixedDeltaTime;

            if (_activeTime > _dashTime)
            {
                DashEnd();
            } 
            if (_activeTime > TimeBreakPoint + Time.fixedDeltaTime)
            {
                //Debug.Log("hallo");
                // float value = _activeTime / (_dashTime / 3);
                // _rb.velocity = new Vector2(_speed * value, 0);
                //float velocity = (_dashDistance.Value / 2) * (-4.5f * (1/(_dashTime*_dashTime)) * _activeTime + 4.5f * 1/_dashTime + 0.5f);
                //Debug.Log(CalculateA() * 2 * (_activeTime - _dashTime));
                float velocity = CalculateA() * 2 * (_activeTime - _dashTime) * _direction;
                Debug.Log(velocity);
                _rb.velocity = new Vector2(velocity, 0);
            }
        }

        private float CalculateA()
        {
            //return (float)-9 / 8 * _dashDistance.Value / (_dashTime * _dashTime);
            
            float boi = TimeBreakPoint - _dashTime;
            return (_breakPoint.Value - _dashDistance.Value)/ (boi * boi);
        }
    }
}