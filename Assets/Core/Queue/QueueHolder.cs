using Core.Actions;
using UnityEngine;

namespace Core.Queue
{
    public class QueueHolder : MonoBehaviour, IQueueHolder
    {
        public ResettableQueue<ICharacterAction> Queue { get; } = new ResettableQueue<ICharacterAction>();
    }
}
