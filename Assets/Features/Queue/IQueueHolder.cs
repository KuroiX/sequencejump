using Features.Actions;
using UnityEngine;

namespace Features.Queue
{
    public interface IQueueHolder
    {
        ResettableQueue<ICharacterAction> Queue { get; }
    }
}