using Core.Actions;
using Core.Queue;
using UnityEngine;

namespace Features.QueueCycle
{
    public class QueueCycleBehaviour : MonoBehaviour
    {
        private ResettableQueue<ICharacterAction> _queue;

        private void Start()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
        }

        public void CycleQueue()
        {
            var action = _queue.Dequeue();
            _queue.Enqueue(action);
        }
    }
}