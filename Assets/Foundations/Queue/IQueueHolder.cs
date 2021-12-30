using Foundations.Actions;

namespace Foundations.Queue
{
    public interface IQueueHolder
    {
        ResettableQueue<ICharacterAction> Queue { get; }
    }
}