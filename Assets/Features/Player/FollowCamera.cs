using UnityEngine;

namespace Features.Player
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] 
        private Transform followTransform;
        [SerializeField] 
        private Rigidbody2D followRb;

        private float _horizontalOffset = 0;
        private float _zOffset = -1;
        
        [SerializeField] 
        private float maxHorizontalOffset;
        [SerializeField] 
        private float horizontalOffsetSpeed;

        private void LateUpdate()
        {
            /*
            if (followRb.velocity.x > 0)
            {
                _horizontalOffset = Mathf.Lerp(_horizontalOffset, maxHorizontalOffset, horizontalOffsetSpeed * Time.deltaTime);
            } 
            else if (followRb.velocity.x < 0)
            {
                _horizontalOffset = Mathf.Lerp(_horizontalOffset, -maxHorizontalOffset, horizontalOffsetSpeed * Time.deltaTime);
            }
            */
            
            transform.position = followTransform.position + Vector3.back + new Vector3(_horizontalOffset, 0, _zOffset);
            
        }
    }
}