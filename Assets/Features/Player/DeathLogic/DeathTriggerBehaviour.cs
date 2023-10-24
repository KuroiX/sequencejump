﻿using System;
using System.Collections;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class DeathTriggerBehaviour : MonoBehaviour, IStopStartSignal
    {
        public event EventHandler Stop;
        public event EventHandler Start;

        public event Action Respawn;

        private void OnRespawn()
        {
            Respawn?.Invoke();
        }

        [SerializeField] private LayerMask hazardMask;
        [SerializeField] private float deathAnimationLength = 1;
        private const float RespawnAnimationLength = 0.8f;

        private bool _isRunning;
        
        private void OnDeathAnimationStart()
        {
            Stop?.Invoke(this, EventArgs.Empty);
        }

        private void OnDeathAnimationEnd()
        {
            Start?.Invoke(this, EventArgs.Empty);
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

            OnRespawn();
            
            yield return new WaitForSeconds(RespawnAnimationLength);
            
            _isRunning = false;
            OnDeathAnimationEnd();
        }
    }
}