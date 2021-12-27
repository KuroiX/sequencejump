using System;
using System.Collections;
using UnityEngine;

namespace Features.Player
{
    public class HazardTriggerEnter : MonoBehaviour
    {
        public static event EventHandler DeathAnimationStart;
        public static event EventHandler DeathAnimationEnd;
        
        [SerializeField] private LayerMask hazardLayerMask;
        [SerializeField] private LayerMask terminalLayerMask;

        private Vector3 _position;

        [SerializeField] private float deathAnimationLength = 1f;

        private SpriteRenderer _sprite;
        private Rigidbody2D _rb;
        private FollowCamera _camera;

        private void Awake()
        {
            _position = transform.position;
            _sprite = GetComponent<SpriteRenderer>();

            _rb = GetComponent<Rigidbody2D>();

            _camera = Camera.main.GetComponent<FollowCamera>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherMask = 1 << other.gameObject.layer;

            if (otherMask == hazardLayerMask)
            {
                if (_isRunning) return;
                StartCoroutine(DeathAnimation());
            }

            if (otherMask == terminalLayerMask)
            {
                _position = other.transform.position;
            }
        }

        private bool _isRunning;

        private IEnumerator DeathAnimation()
        {
            _isRunning = true;
            OnDeathAnimationStart();
            
            _camera.enabled = false;
            //_rb.velocity = Vector2.zero;
            //float scale = _rb.gravityScale;
            //_rb.gravityScale = 0;
            _sprite.enabled = false;
            
            yield return new WaitForSeconds(deathAnimationLength);
            
            transform.position = _position;
            _sprite.enabled = true;
            _camera.enabled = true;
            _isRunning = false;
            
            OnDeathAnimationEnd();
        }

        private void OnDeathAnimationStart()
        {
            DeathAnimationStart?.Invoke(this, EventArgs.Empty);
        }

        private void OnDeathAnimationEnd()
        {
            DeathAnimationEnd?.Invoke(this, EventArgs.Empty);
        }
    }
}
