using Features.Actions;
using Features.Queue;

namespace Features.Station
{
    public class Station
    {
        public static Station CurrentStation { get; private set; }

        public readonly ActionCounter ActionCounter;

        // Optional
        private int _maxAssignableCount;
        public int AssignableCount;
        public bool HasAssignableCount;
        
        private ResettableQueue<CharacterAction> _queue;

        public Station(StationSettings settings, ResettableQueue<CharacterAction> queue)
        {
            _maxAssignableCount = settings.maxAssignableActions;
            AssignableCount = _maxAssignableCount;
            HasAssignableCount = _maxAssignableCount != 0;
            
            _queue = queue;
            
            ActionCounter = new ActionCounter(settings.actionCounts);
        }

        public void OpenStation()
        {
            if (CurrentStation != this)
            {
                // empty queue
            }
            
            CurrentStation = this;
        }

        public void HandleOnTriggerEnter()
        {
            if (CurrentStation == this)
            {
                _queue.ResetQueue();
            }
        }

        // Called from button/interface?
        public void EnqueueAction(CharacterAction characterAction)
        {
            _queue.Enqueue(characterAction);
            ActionCounter.RemoveAction(characterAction);
        }
    }
}