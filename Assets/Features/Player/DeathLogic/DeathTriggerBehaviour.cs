using System;
using System.Collections;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class DeathTriggerBehaviour : MonoBehaviour, IStopStartSignal
    {
        public event EventHandler Stop
        {
            add => DeathStart += value;
            remove => DeathStart -= value;
        }
        
        public event EventHandler Start
        {
            add => DeathEnd += value;
            remove => DeathEnd -= value;
        }
        
        public static event EventHandler DeathStart;
        public static event EventHandler DeathEnd;

        [SerializeField] private LayerMask hazardMask;
        [SerializeField] private float deathAnimationLength = 1;

        private bool _isRunning;
        
        private void OnDeathAnimationStart()
        {
            DeathStart?.Invoke(this, EventArgs.Empty);
        }

        private void OnDeathAnimationEnd()
        {
            DeathEnd?.Invoke(this, EventArgs.Empty);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherMask = 1 << other.gameObject.layer;
            
            if (otherMask == hazardMask)
            {
                if (_isRunning) return;
                StartCoroutine(DeathAnimation());
            }
        }
        
        private IEnumerator DeathAnimation()
        {
            _isRunning = true;
            OnDeathAnimationStart();

            yield return new WaitForSeconds(deathAnimationLength);
            
            _isRunning = false;
            OnDeathAnimationEnd();
        }
    }
}