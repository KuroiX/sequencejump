using UnityEngine;

namespace Features.Player
{
    public class HazardTriggerEnter : MonoBehaviour
    {
        [SerializeField] private LayerMask hazardLayerMask;
        [SerializeField] private LayerMask terminalLayerMask;

        private Vector3 _position;

        private void Awake()
        {
            _position = transform.position;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherMask = 1 << other.gameObject.layer;

            if (otherMask == hazardLayerMask)
            {
                transform.position = _position;
            }

            if (otherMask == terminalLayerMask)
            {
                _position = other.transform.position;
            }
        }
    }
}
