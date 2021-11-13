using System;
using System.Linq;
using Features.Actions;
using Features.Queue;

namespace Features.Station
{
    public class Station
    {
        public static Station CurrentStation { get; private set; }
        
        public static event EventHandler StationEntered;
        public static event EventHandler StationOpened;
        public static event EventHandler StationChanged;
        public static event EventHandler StationClosed;
        public static event EventHandler StationExited;

        
        public readonly InstanceCounter<CharacterAction> ActionCounter;

        // Optional
        public readonly bool HasAssignableCount;
        public int AssignableCount;
        private readonly int _maxAssignableCount;

        private ResettableQueue<CharacterAction> _queue;
        // :(
        private readonly StationEventArgs _args;

        public Station(StationSettings settings, ResettableQueue<CharacterAction> queue)
        {
            _maxAssignableCount = settings.maxAssignableActions;
            AssignableCount = _maxAssignableCount;
            HasAssignableCount = _maxAssignableCount != 0;
            
            _queue = queue;

            // TODO: UGLY AF, SHOULD BE MOVED TO CUSTOM INSPECTOR ASAP
            int size = CharacterAction.CharacterActions.Count;
            int inputSize = settings.actionCounts.Length;

            CharacterAction[] actions = new CharacterAction[size];
            int[] count = new int[size];
            
            int i = 0;
            for (; i < inputSize; i++)
            {
                actions[i] = settings.actionCounts[i].CharacterAction;
                count[i] = settings.actionCounts[i].Count;
            }

            foreach (var value in CharacterAction.CharacterActions.Values)
            {
                if (!actions.Contains(value))
                {
                    actions[i] = value;
                    count[i] = 0;
                    i++;
                }
            }
            // --------------------------------------------------------
            
            ActionCounter = new InstanceCounter<CharacterAction>(actions, count);
            
            _args = new StationEventArgs(this);
        }

        private void OnStationEntered()
        {
            StationEntered?.Invoke(this, _args);
        }
        
        private void OnStationOpened()
        {
            StationOpened?.Invoke(this, _args);
        }
        
        private void OnStationChanged()
        {
            StationChanged?.Invoke(this, _args);
        }

        private void OnStationClosed()
        {
            StationClosed?.Invoke(this, _args);
        }
        
        private void OnStationExited()
        {
            StationExited?.Invoke(this, _args);
        }

        public void OpenStation()
        {
            if (CurrentStation != this)
            {
                _queue.Clear();
                ResetStation();
            }
            
            CurrentStation = this;
            OnStationOpened();
        }

        public void CloseStation()
        {
            OnStationClosed();
        }
        
        public void HandleOnTriggerEnter()
        {
            if (CurrentStation == this)
            {
                _queue.ResetQueue();
            }
            OnStationEntered();
        }

        public void HandleOnTriggerExit()
        {
            OnStationExited();
        }
        
        public void EnqueueAction(CharacterAction characterAction)
        {
            bool isAllowedToEnqueue = (!HasAssignableCount || AssignableCount > 0) && ActionCounter.RemoveInstance(characterAction);
            
            if (!isAllowedToEnqueue) return;
            
            _queue.Enqueue(characterAction);
            AssignableCount -= 1;
            
            OnStationChanged();
        }

        public void ResetStation()
        {
            _queue.Clear();
            ActionCounter.ResetCurrentAvailableInstances();
            AssignableCount = _maxAssignableCount;
            
            OnStationChanged();
        }
    }
}