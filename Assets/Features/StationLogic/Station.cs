using System;
using System.Collections.Generic;
using Core.Actions;
using Core.Queue;

namespace Features.StationLogic
{
    public class Station
    {
        public static Station CurrentStation { get; private set; }
        
        public static event EventHandler StationEntered;
        public static event EventHandler StationOpened;
        public static event EventHandler StationChanged;
        public static event EventHandler StationClosed;
        public static event EventHandler StationExited;

        
        public Dictionary<ICharacterAction, int> AvailableCount => _actionCounter.AvailableCount;
        public Dictionary<ICharacterAction, int> CurrentCount => _actionCounter.CurrentCount;
        
        private readonly InstanceCounter<ICharacterAction> _actionCounter;

        public bool HasAssignableCount => _maxAssignableCount > 0;
        public int AssignableCount { get; private set; }
        private readonly int _maxAssignableCount;

        private readonly ResettableQueue<ICharacterAction> _queue;

        public Station(
            ResettableQueue<ICharacterAction> queue, 
            InstanceCounter<ICharacterAction> counter,
            int maxAssignableCount = 0)
        {
            if (maxAssignableCount < 0)
            {
                throw new ArgumentException($"maxAssignableCount ({maxAssignableCount}) can never be negative.");
            }
            
            _queue = queue;
            _actionCounter = counter;
            
            _maxAssignableCount = maxAssignableCount;
            AssignableCount = _maxAssignableCount;
        }

        #region Event Methods
        
        private void OnStationEntered()
        {
            StationEntered?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnStationOpened()
        {
            StationOpened?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnStationChanged()
        {
            StationChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnStationClosed()
        {
            StationClosed?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnStationExited()
        {
            StationExited?.Invoke(this, EventArgs.Empty);
        }
        
        #endregion
        
        public void HandleOnTriggerEnter()
        {
            if (CurrentStation == this)
            {
                _queue.ResetQueue();
            }
            OnStationEntered();
        }
        
        public void Open()
        {
            if (CurrentStation != this)
            {
                Reset();
                CurrentStation = this;
            }
            
            OnStationOpened();
        }
        
        public void Reset()
        {
            _queue.Clear();
            _actionCounter.Reset();
            AssignableCount = _maxAssignableCount;
            
            OnStationChanged();
        }

        public void EnqueueAction(ICharacterAction characterAction)
        {
            bool isAllowedToEnqueue = (!HasAssignableCount || AssignableCount > 0) && _actionCounter.Remove(characterAction);
            
            if (!isAllowedToEnqueue) return;
            
            _queue.Enqueue(characterAction);
            if (HasAssignableCount)
            {
                AssignableCount -= 1;
            }
            
            OnStationChanged();
        }

        public void Close()
        {
            OnStationClosed();
        }

        public void HandleOnTriggerExit()
        {
            OnStationExited();
        }
    }
}