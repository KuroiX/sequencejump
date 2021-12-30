using System;
using UnityEngine;
using UnityEngine.UI;
using Foundations.Actions;

namespace Foundations.Queue
{
    public class QueueUIHandler : MonoBehaviour
    {
        [SerializeField] private Image first;
        [SerializeField] private Image second;
        [SerializeField] private Image third;
        [SerializeField] private Image forth;

        private ResettableQueue<ICharacterAction> _queue;

        private void Awake()
        {
            // TODO: better injection? :(
            _queue = FindObjectOfType<QueueHolder>().Queue;
        }

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
            int currentPosition = _queue.Count - 1;

            switch (currentPosition)
            {
                case 0:
                    second.sprite = _queue.Peek().Sprite;
                    break;
                case 1:
                    third.sprite = _queue.Peek(1).Sprite;
                    break;
                case 2:
                    forth.sprite = _queue.Peek(2).Sprite;
                    break;
                default:
                    Debug.Log("Out of range");
                    break;
            }
        }
        
        private void OnItemDequeued(object sender, EventArgs args)
        {
            first.sprite = second.sprite;
            second.sprite = third.sprite;
            third.sprite = forth.sprite;

            ICharacterAction action = _queue.Peek(3);

            forth.sprite = action?.Sprite;
            Debug.Log("Dequeued");
        }
        
        private void OnQueueReset(object sender, EventArgs args)
        {
            first.sprite = null;
            second.sprite = _queue.Peek()?.Sprite;
            third.sprite = _queue.Peek(1)?.Sprite;
            forth.sprite = _queue.Peek(2)?.Sprite;
            Debug.Log("Reset");
        }
        
        private void OnQueueCleared(object sender, EventArgs args)
        {
            first.sprite = null;
            second.sprite = null;
            third.sprite = null;
            forth.sprite = null;
            Debug.Log("Cleared");
        }
    }
}