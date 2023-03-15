using UnityEngine;

namespace Features.Player.Controller
{
    public class DynamicDashController : IDashController
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

        public DynamicDashController(Rigidbody2D rb, Ref<int> iterations, Ref<float> dashDistance, Ref<float> breakPoint)
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

            CalculateDistance();
            
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
            else if (_activeTime > TimeBreakPoint + Time.deltaTime)
            {
                float velocity = CalculateA() * 2 * (_activeTime - _dashTime) * _direction;
                _rb.velocity = new Vector2(velocity, 0);
            }
            else if (_activeTime > TimeBreakPoint)
            {
                float velocity = CalculateA() * 2 * (_activeTime - _dashTime);
                velocity += CalculateDistance();
                velocity *= _direction;
                _rb.velocity = new Vector2(velocity, 0);
            }
        }

        private float CalculateA()
        {
            //return (float)-9 / 8 * _dashDistance.Value / (_dashTime * _dashTime);
            
            float boi = TimeBreakPoint - _dashTime;
            return (_breakPoint.Value - _dashDistance.Value)/ (boi * boi);
        }

        private float CalculateDistance()
        {
            float s = 0;
            float timePassed = 0;

            float linearVelocity = TimeBreakPoint == 0
                ? CalculateA() * 2 * (-_dashTime)
                : _breakPoint.Value / TimeBreakPoint;
            
            for (int i = 0; i < _iterations.Value; i++)
            {
                timePassed += Time.fixedDeltaTime;
                
                float velocity = timePassed < TimeBreakPoint ? linearVelocity : CalculateA() * 2 * (timePassed - _dashTime);

                s += velocity * Time.fixedDeltaTime;
            }

            return (_dashDistance.Value - s) / Time.fixedDeltaTime;
        }
    }
}