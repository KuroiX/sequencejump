using System;
using Core.Actions;
using UnityEngine;

namespace Core.Queue
{
    public class QueueUIHandler : MonoBehaviour
    {
        [SerializeField] private QueueAction[] queueActions;

        private int _index;

        private bool _isFresh;

        private ResettableQueue<ICharacterAction> _queue;

        private void Awake()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
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
            
            queueActions[Index(currentPosition)].Sprite = _queue.Peek(currentPosition-1).Sprite;
            queueActions[Index(currentPosition)].AnimateEnqueue(currentPosition);
        }
        
        private void OnItemDequeued(object sender, EventArgs args)
        {
            for (int i = 0; i < queueActions.Length; i++)
            {
                //Debug.Log(Index(i) + " " + Index(i-1));
                queueActions[Index(i)].StartAnimation(i, Index(i-1 - _index));
            }
            
            ICharacterAction action = _queue.Peek(queueActions.Length-1);

            queueActions[Index(queueActions.Length)].Sprite = action?.Sprite;

            Increment();

            _isFresh = false;

            //Debug.Log("Dequeued");
        }
        
        private void OnQueueReset(object sender, EventArgs args)
        {
            if (_isFresh) return;
            
            queueActions[Index(0)].Sprite = null;
            
            for (int i = 1; i < queueActions.Length; i++)
            {
                queueActions[Index(i)].Sprite = _queue.Peek(i-1)?.Sprite;
                queueActions[Index(i)].AnimateEnqueue(i);
            }

            _isFresh = true;

            //Debug.Log("Reset");
        }
        
        private void OnQueueCleared(object sender, EventArgs args)
        {
            for (int i = 0; i < queueActions.Length; i++)
            {
                //queueActions[Index(i)].Sprite = null;
                queueActions[Index(i)].AnimateReset(i);
            }
            
            //Debug.Log("Cleared");
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