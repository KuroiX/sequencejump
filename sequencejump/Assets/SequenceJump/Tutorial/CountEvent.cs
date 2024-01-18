using System;
using SequenceJump.Abilities;
using SequenceJump.Queue;
using SequenceJump.StationLogic;

namespace SequenceJump.Tutorial
{
    public class CountEvent
    {
        public event EventHandler ClosedAndFinished;
        public event EventHandler ClosedAndNotFinished;
        
        private readonly ResettableQueue<ICharacterAction> _queue;
        
        private readonly int _countToReach;
        private int _count;

        public CountEvent(int countToReach, ResettableQueue<ICharacterAction> queue)
        {
            _count = 0;
            _countToReach = countToReach;
            _queue = queue;
            _queue.ItemEnqueued += OnItemEnqueued;
            Station.StationClosed += OnClosed;
        }
        
        private void OnItemEnqueued(object sender, EventArgs e)
        {
            _count++;
            if (_count == _countToReach)
            {
                _queue.ItemEnqueued -= OnItemEnqueued;
            }
        }
        
        private void OnClosed(object sender, EventArgs e)
        {
            if (_count == _countToReach)
            {
                ClosedAndFinished?.Invoke(this, EventArgs.Empty);
                Station.StationClosed -= OnClosed;
            }
            else
            {
                ClosedAndNotFinished?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}