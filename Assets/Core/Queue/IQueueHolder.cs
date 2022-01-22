using Core.Actions;

namespace Core.Queue
{
    public interface IQueueHolder
    {
        ResettableQueue<ICharacterAction> Queue { get; }
    }
}