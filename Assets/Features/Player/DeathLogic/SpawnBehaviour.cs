using System;
using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class SpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private Component respawnSignal;
        [SerializeField] private LayerMask checkpointLayerMask;

        private IStartSignal _signal;

        private Vector3 _position;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _position = _transform.position;
            _signal = (IStartSignal) respawnSignal;
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
            _signal.Start += Respawn;
        }

        private void OnDisable()
        {
            _signal.Start -= Respawn;
        }
        
        private void Respawn(object sender, EventArgs args)
        {
            _transform.position = _position;
        }
    }
}
