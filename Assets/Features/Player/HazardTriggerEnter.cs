using UnityEngine;

namespace Features.Player
{
    public class HazardTriggerEnter : MonoBehaviour
    {
        [SerializeField] private LayerMask hazardLayerMask;

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
        }
    }
}
