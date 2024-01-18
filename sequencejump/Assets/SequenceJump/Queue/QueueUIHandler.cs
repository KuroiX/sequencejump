using System;
using SequenceJump.Abilities;
using UnityEngine;

namespace SequenceJump.Queue
{
    public class QueueUIHandler : MonoBehaviour
    {
        [SerializeField] private QueueActionUiElement[] queueActions;

        private int _index;

        private bool _isFresh;

        private ResettableQueue<ICharacterAction> _queue;

        private void Awake()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
            QueueActionUiElement.QueueActions = queueActions;
            _index = 0;
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
            int currentPosition = _queue.Count;

            if (currentPosition >= queueActions.Length) return;
            
            queueActions[Index(currentPosition)].AnimateEnqueue(currentPosition, _queue.Peek(currentPosition-1).Sprite);
        }
        
        private void OnItemDequeued(object sender, EventArgs args)
        {
            AnimateAllDequeue();
            
            SetNextSprite();

            Increment();

            _isFresh = false;
        }

        private void SetNextSprite()
        {
            ICharacterAction action = _queue.Peek(queueActions.Length - 1);

            queueActions[Index(queueActions.Length)].Sprite = action?.Sprite;
        }

        private void AnimateAllDequeue()
        {
            for (int i = 0; i < queueActions.Length; i++)
            {
                queueActions[Index(i)].AnimateDequeue(i, Index(i - 1 - _index));
            }
        }

        private void OnQueueReset(object sender, EventArgs args)
        {
            if (_isFresh) return;
            
            queueActions[Index(0)].Sprite = null;
            
            for (int i = 1; i < queueActions.Length; i++)
            {
                queueActions[Index(i)].AnimateEnqueue(i, _queue.Peek(i-1)?.Sprite);
            }

            _isFresh = true;
        }
        
        private void OnQueueCleared(object sender, EventArgs args)
        {
            for (int i = 0; i < queueActions.Length; i++)
            {
                queueActions[Index(i)].AnimateClear(i);
            }
        }

        private int Index(int index)
        {
            return (_index + index + queueActions.Length) % queueActions.Length;
        }

        private void Increment()
        {
            _index = Index(1);
        }
    }
}