using System;
using Features.Actions;
using Features.Queue;
using UnityEngine;

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

        
        public readonly ActionCounter ActionCounter;

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
            
            ActionCounter = new ActionCounter(settings.actionCounts);
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
            bool isAllowedToEnqueue = (!HasAssignableCount || AssignableCount > 0) && ActionCounter.RemoveAction(characterAction);
            
            if (!isAllowedToEnqueue) return;
            
            _queue.Enqueue(characterAction);
            AssignableCount -= 1;
            
            OnStationChanged();
        }

        public void ResetStation()
        {
            _queue.Clear();
            ActionCounter.ResetCurrentAvailableActions();
            AssignableCount = _maxAssignableCount;
            
            OnStationChanged();
        }
    }
}