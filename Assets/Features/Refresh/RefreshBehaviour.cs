using Core.Actions;
using Core.Queue;
using Features.StationLogic;
using UnityEngine;

namespace Features.Refresh
{
    public class RefreshBehaviour : StationEnteredReceiverBehaviour
    {
        private ResettableQueue<ICharacterAction> _queue;
        private SpriteRenderer _spriteRenderer;
        private bool _isEnabled = true;
        
        private void Start()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
            _spriteRenderer.color = Color.gray;
        }

        public override void ReceiveStationEntered()
        {
            _isEnabled = true;
            _spriteRenderer.color = Color.green;
        }
    }
}