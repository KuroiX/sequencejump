using System;
using Core.Actions;
using UnityEngine;

namespace Core.Queue
{
    public class QueueUIHandler : MonoBehaviour
    {
        [SerializeField] private QueueAction[] queueActions;

        private int _index;

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
        }
        
        private void OnItemDequeued(object sender, EventArgs args)
        {
            for (int i = 0; i < queueActions.Length; i++)
            {
                queueActions[i].StartAnimation();
            }
            
            ICharacterAction action = _queue.Peek(queueActions.Length);

            queueActions[Index(queueActions.Length - 1)].Sprite = action?.Sprite;

            _index = (_index + 1) % queueActions.Length;

            //Debug.Log("Dequeued");
        }
        
        private void OnQueueReset(object sender, EventArgs args)
        {
            queueActions[Index(0)].Sprite = null;
            
            for (int i = 1; i < queueActions.Length; i++)
            {
                queueActions[Index(i)].Sprite = _queue.Peek(i-1)?.Sprite;
            }

            //Debug.Log("Reset");
        }
        
        private void OnQueueCleared(object sender, EventArgs args)
        {
            for (int i = 0; i < queueActions.Length; i++)
            {
                queueActions[Index(i)].Sprite = null;
            }
            
            //Debug.Log("Cleared");
        }

        private int Index(int index)
        {
            return (_index + index) % queueActions.Length;
        }
    }
}