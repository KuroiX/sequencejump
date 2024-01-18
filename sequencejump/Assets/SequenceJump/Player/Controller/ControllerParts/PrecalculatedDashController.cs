using UnityEngine;

namespace SequenceJump.Player.Controller.ControllerParts
{
    public class PrecalculatedDashController : IDashController
    {
        public bool IsDashing { get; }

        private Rigidbody2D _rb;
        private float _initialGravityScale;
        private float[] _speeds;
        
        public PrecalculatedDashController(Rigidbody2D rb, int iterations, float dashDistance, float breakPoint)
        {
            _rb = rb;
            _initialGravityScale = rb.gravityScale;

            _speeds = new float[iterations];
            
            // float linearVelocity = TimeBreakPoint == 0
            //     ? CalculateA() * 2 * (-_dashTime)
            //     : _breakPoint.Value / TimeBreakPoint;
            //
            // for (int i = 0; i < iterations; i++)
            // {
            //     timePassed += Time.fixedDeltaTime;
            //     
            //     float velocity = timePassed < TimeBreakPoint ? linearVelocity : CalculateA() * 2 * (timePassed - _dashTime);
            //
            //     s += velocity * Time.fixedDeltaTime;
            // }
            //
            // _dashDistance = dashDistance;
            // _breakPoint = breakPoint;
        }
        
        private float CalculateSpeeds()
        {
            // float s = 0;
            // float timePassed = 0;
            //
            // float linearVelocity = TimeBreakPoint == 0
            //     ? CalculateA() * 2 * (-_dashTime)
            //     : _breakPoint.Value / TimeBreakPoint;
            //
            // for (int i = 0; i < _iterations.Value; i++)
            // {
            //     timePassed += Time.fixedDeltaTime;
            //     
            //     float velocity = timePassed < TimeBreakPoint ? linearVelocity : CalculateA() * 2 * (timePassed - _dashTime);
            //
            //     s += velocity * Time.fixedDeltaTime;
            // }
            //
            // return (_dashDistance.Value - s) / Time.fixedDeltaTime;
            return 0f;
        }
        
        public void Dash(float direction)
        {
            throw new System.NotImplementedException();
        }

        public void DashEnd()
        {
            throw new System.NotImplementedException();
        }

        public void HandleFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}