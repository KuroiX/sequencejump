using Features.Actions;
using UnityEngine;

namespace Features.Queue
{
    public class QueueHolder : MonoBehaviour, IQueueHolder
    {
        public ResettableQueue<ICharacterAction> Queue { get; } = new ResettableQueue<ICharacterAction>();
    }
}
