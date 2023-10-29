using UnityEngine;

namespace Features.ControllablePlatform
{
    public class PlayerPareting : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag("Player"))
                return;

            other.transform.parent = transform;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.CompareTag("Player"))
                return;

            if (!other.GetComponent<Rigidbody2D>().simulated) return;
            
            other.transform.parent = null;
        }
    }
}
