using System;
using Features.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Queue
{
    public class QueueUIHandler : MonoBehaviour
    {
        [SerializeField] private Image first;
        [SerializeField] private Image second;
        [SerializeField] private Image third;
        [SerializeField] private Image forth;

        private ResettableQueue<ICharacterAction> _queue;

        private void OnEnable()
        {
            _queue.ItemEnqueued += OnItemEnqueued;
            _queue.ItemDequeued += OnItemDequeued;
            
            _queue.QueueReset += OnQueueReset;
            _queue.QueueCleared += OnQueueCleared;
        }

        private void OnDisable()
        {
            _queue.ItemEnqueued -= OnItemEnqueued;
            _queue.ItemDequeued -= OnItemDequeued;
            
            _queue.QueueReset -= OnQueueReset;
            _queue.QueueCleared -= OnQueueCleared;
        }

        private void OnItemEnqueued(object sender, EventArgs args)
        {
            
        }
        
        private void OnItemDequeued(object sender, EventArgs args)
        {
            first.sprite = second.sprite;
            second.sprite = third.sprite;
            third.sprite = forth.sprite;

            ICharacterAction action = _queue.Peek(3);

            forth.sprite = action.Sprite;
        }
        
        private void OnQueueReset(object sender, EventArgs args)
        {
            first.sprite = null;
            second.sprite = _queue.Peek()?.Sprite;
            third.sprite = _queue.Peek(1)?.Sprite;
            forth.sprite = _queue.Peek(2)?.Sprite;
        }
        
        private void OnQueueCleared(object sender, EventArgs args)
        {
            first.sprite = null;
            second.sprite = null;
            third.sprite = null;
            forth.sprite = null;
        }
    }
}