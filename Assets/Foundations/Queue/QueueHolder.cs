using UnityEngine;
using Foundations.Actions;

namespace Foundations.Queue
{
    public class QueueHolder : MonoBehaviour, IQueueHolder
    {
        public ResettableQueue<ICharacterAction> Queue { get; } = new ResettableQueue<ICharacterAction>();
    }
}
