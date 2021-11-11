using System.Collections.Generic;
using Features.Player;

namespace Features.Station
{
    public class Station
    {
        private static Station _currentStation;

        public readonly ActionCounter ActionCounter;

        private ResettableQueue<Action> _queue;

        public Station(ResettableQueue<Action> queue)
        {
            _queue = queue;
            ActionCounter = new ActionCounter();
        }

        public void OpenStation()
        {
            if (_currentStation != this)
            {
                // empty queue
            }
            
            _currentStation = this;
        }

        public void HandleOnTriggerEnter()
        {
            if (_currentStation == this)
            {
                _queue.ResetQueue();
            }
        }

        // Called from button/interface?
        public void EnqueueAction(Action action)
        {
            _queue.Enqueue(action);
        }
    }
}