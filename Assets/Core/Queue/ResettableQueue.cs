using System;
using System.Collections.Generic;

namespace Core.Queue
{
    /// <summary>
    /// Same functionality as a queue, but information is not lost so it can be reset.
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class ResettableQueue<T>
    {
        public event EventHandler ItemEnqueued;
        public event EventHandler ItemDequeued;
        public event EventHandler QueueReset;
        public event EventHandler QueueCleared;

        public int Size => _list.Count;
        public int Count => Size - _index;

        private readonly List<T> _list;
        private int _index;

        public ResettableQueue()
        {
            _list = new List<T>();
            _index = 0;
        }

        private void OnItemEnqueued()
        {
            ItemEnqueued?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnItemDequeued()
        {
            ItemDequeued?.Invoke(this, EventArgs.Empty);
        }

        private void OnQueueReset()
        {
            QueueReset?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnQueueCleared()
        {
            QueueCleared?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Enqueues an item
        /// </summary>
        /// <param name="item">Item that gets enqueued</param>
        /// <returns>true if successful, false if not successful</returns>
        public void Enqueue(T item)
        {
            _list.Add(item);
            OnItemEnqueued();
        }

        /// <summary>
        /// Dequeues the next item
        /// </summary>
        /// <returns>Item that gets dequeued</returns>
        public T Dequeue()        
        {
            OnItemDequeued();
            
            if (IsEmpty())
                return default;
        
            return _list[_index++];
        }

        /// <summary>
        /// Shows the next item to be dequeued
        /// </summary>
        /// <returns>Next item to be dequeued</returns>
        public T Peek(int i = 0)
        {
            if (IsEmpty() || _index + i >= Size)
                return default;
        
            return _list[_index+i];
        }

        /// <summary>
        /// Tells you if the queue is empty
        /// </summary>
        /// <returns>True</returns>
        public bool IsEmpty()
        {
            return _index >= _list.Count;
        }

        /// <summary>
        /// Resets the queue
        /// </summary>
        public void ResetQueue()
        {
            _index = 0;
            OnQueueReset();
        }

        /// <summary>
        /// Clears the current queue
        /// </summary>
        public void Clear()
        {
            _list.Clear();
            _index = 0;
            OnQueueCleared();
        }
    }
}