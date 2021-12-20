using UnityEngine;

namespace Features.Player
{
    public abstract class ActionController
    {
        private Rigidbody2D _rb;

        public bool IsDoing { get; } = false;
        
        protected ActionController(Rigidbody2D rb)
        {
            _rb = rb;
        }

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void HandleFixedUpdate();

        public abstract void HandleUpdate();
    }
}