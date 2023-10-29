using System;

namespace Features.Tutorial
{
    public class TutorialState
    {
        public static void Connect(TutorialState prev, TutorialState next)
        {
            prev._next = next;
            next._prev = prev;
        }
        
        private readonly Action<EventHandler> _nextSubHandler;
        private readonly Action<EventHandler> _nextUnsubHandler;
        private readonly Action<EventHandler> _prevSubHandler;
        private readonly Action<EventHandler> _prevUnsubHandler;
        private readonly Action _onStateEnter;
        private readonly Action _onStateExit;

        private TutorialState _next;
        private TutorialState _prev;

        private bool _isBeingDestroyed;

        public TutorialState(
            Action<EventHandler> nextSubHandler, Action<EventHandler> nextUnsubHandler, 
            Action<EventHandler> prevSubHandler, Action<EventHandler> prevUnsubHandler, 
            Action onStateEnter, Action onStateExit)
        {
            _nextSubHandler = nextSubHandler;
            _nextUnsubHandler = nextUnsubHandler;
            _prevSubHandler = prevSubHandler;
            _prevUnsubHandler = prevUnsubHandler;
            _onStateEnter = onStateEnter;
            _onStateExit = onStateExit;
        }

        public void Setup()
        {
            _onStateEnter?.Invoke();
            _nextSubHandler?.Invoke(OnNext);
            _prevSubHandler?.Invoke(OnPrev);
        }

        public virtual void Teardown(bool isBeingDestroyed)
        {
            if (!isBeingDestroyed)
            {
                _onStateExit?.Invoke();
            }
            _nextUnsubHandler?.Invoke(OnNext);
            _prevUnsubHandler?.Invoke(OnPrev);
        }
        
        private void OnNext(object sender, EventArgs args)
        {
            Teardown(false);
            
            _next?.Setup();
        }
        
        private void OnPrev(object sender, EventArgs args)
        {
            Teardown(false);
            _prev?.Setup();
        }
    }
}