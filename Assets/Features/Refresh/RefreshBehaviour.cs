using System;
using Core.Actions;
using Core.Queue;
using Features.StationLogic;
using UnityEngine;

namespace Features.Refresh
{
    public class RefreshBehaviour : StationEnteredReceiverBehaviour
    {
        [SerializeField] private Color activatedColor;
        [SerializeField] private Color deactivatedColor;

        private ResettableQueue<ICharacterAction> _queue;
        private SpriteRenderer _spriteRenderer;
        private bool _isEnabled = true;

        private Animator _animator;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = activatedColor;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_isEnabled) return;
            
            _queue.ResetQueue();
            SetDisabled();
        }

        private void SetDisabled()
        {
            _isEnabled = false;
            _spriteRenderer.color = deactivatedColor;
            
            _animator.SetTrigger("Collected");
        }

        public override void ReceiveStationEntered()
        {
            if (_isEnabled) return;
            
            _isEnabled = true;
            _spriteRenderer.color = activatedColor;
            
            _animator.SetTrigger("Refresh");
        }
    }
}