using SequenceJump.Abilities;
using UnityEngine;

namespace SequenceJump.Queue
{
    public class QueueHolder : MonoBehaviour, IQueueHolder
    {
        public ResettableQueue<ICharacterAction> Queue { get; } = new ResettableQueue<ICharacterAction>();
    }
}
