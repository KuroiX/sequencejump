using System;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class SpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private Component respawnSignal;
        [SerializeField] private LayerMask checkpointLayerMask;

        private DeathTriggerBehaviour _signal;

        private Vector3 _position;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _position = _transform.position;
            _signal = (DeathTriggerBehaviour) respawnSignal;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherMask = 1 << other.gameObject.layer;

            if (otherMask == checkpointLayerMask)
            {
                _position = other.transform.position;
            }
        }

        private void OnEnable()
        {
            _signal.Respawn += Respawn;
        }

        private void OnDisable()
        {
            _signal.Respawn -= Respawn;
        }
        
        private void Respawn()
        {
            _transform.position = _position;
        }
    }
}
