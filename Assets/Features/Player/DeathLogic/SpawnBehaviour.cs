using UnityEngine;

namespace Features.Player.DeathLogic
{
    public class SpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private DeathTriggerBehaviour respawnSignal;
        [SerializeField] private LayerMask checkpointLayerMask;

        private Vector3 _position;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _position = _transform.position;
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
            respawnSignal.Respawn += Respawn;
        }

        private void OnDisable()
        {
            respawnSignal.Respawn -= Respawn;
        }
        
        private void Respawn()
        {
            _transform.position = _position;
            // hotfix TODO: remove when platforms are fixed
            _transform.parent = null;
        }
    }
}
