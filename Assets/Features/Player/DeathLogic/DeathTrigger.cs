using System;
using System.Collections;
using Features.Other;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class DeathTrigger : MonoBehaviour, IStartEndEvent
    {
        public event EventHandler Activated
        {
            add => DeathStart += value;
            remove => DeathStart -= value;
        }
        
        public event EventHandler Deactivated
        {
            add => DeathEnd += value;
            remove => DeathEnd -= value;
        }
        
        public event EventHandler DeathStart;
        public event EventHandler DeathEnd;

        [SerializeField] private LayerMask hazardMask;
        [SerializeField] private float deathAnimationLength;

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