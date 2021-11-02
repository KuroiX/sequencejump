using UnityEngine;

namespace Features.Player
{
    public class HazardTriggerEnter : MonoBehaviour
    {
        [SerializeField] private LayerMask hazardLayerMask;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherMask = 1 << other.gameObject.layer;

            if (otherMask == hazardLayerMask)
            {
                Debug.Log("You died!");
            }
        }
    }
}
