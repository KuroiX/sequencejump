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
        
        private readonly Action<EventHandler, bool> _nextHandler;
        private readonly Action<EventHandler, bool> _prevHandler;
        private readonly Action<bool> _onState;

        private TutorialState _next;
        private TutorialState _prev;

        public TutorialState(Action<EventHandler, bool> nextHandler, Action<EventHandler, bool> prevHandler, Action<bool> onState)
        {
            _nextHandler = nextHandler;
            _prevHandler = prevHandler;
            _onState = onState;
        }

        public void Setup()
        {
            _onState?.Invoke(true);
            _nextHandler?.Invoke(OnNext, true);
            _prevHandler?.Invoke(OnPrev, true);
        }

        private void Teardown()
        {
            _onState?.Invoke(false);
            _nextHandler?.Invoke(OnNext, false);
            _prevHandler?.Invoke(OnPrev, false);
        }
        
        private void OnNext(object sender, EventArgs args)
        {
            Teardown();
            _next?.Setup();
        }
        
        private void OnPrev(object sender, EventArgs args)
        {
            Teardown();
            _prev?.Setup();
        }
    }
}