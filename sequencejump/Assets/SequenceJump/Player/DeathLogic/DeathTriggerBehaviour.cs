using System;
using System.Collections;
using SequenceJump.Tools;
using UnityEngine;

namespace SequenceJump.Player.DeathLogic
{
    public class DeathTriggerBehaviour : TimedSignalBehaviour
    {
        public event Action Respawn;

        [SerializeField] private LayerMask hazardMask;
        private const float RespawnAnimationLength = 0.8f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherMask = 1 << other.gameObject.layer;

            if (otherMask != hazardMask) return;
            
            if (isRunning) return;
            
            StartCoroutine(DeathAnimation());
            StartCoroutine(StartTriggered());
        }
        
        private IEnumerator DeathAnimation()
        {
            yield return new WaitForSeconds(timeToStop - RespawnAnimationLength);

            OnRespawn();
        }
        
        private void OnRespawn()
        {
            Respawn?.Invoke();
        }
    }
}