using SequenceJump.Abilities;

namespace SequenceJump.Queue
{
    public interface IQueueHolder
    {
        ResettableQueue<ICharacterAction> Queue { get; }
    }
}