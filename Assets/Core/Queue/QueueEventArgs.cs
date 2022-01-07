using System;

namespace Core.Queue
{
    public class QueueEventArgs<T> : EventArgs
    {
        public ResettableQueue<T> Queue { get; }
        
        public QueueEventArgs(ResettableQueue<T> queue)
        {
            Queue = queue;
        }
    }
}