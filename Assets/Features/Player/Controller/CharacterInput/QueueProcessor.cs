using Core.Actions;
using Core.Queue;


namespace Features.Player.Controller.CharacterInput
{
    public class QueueProcessor
    {
        private readonly ResettableQueue<ICharacterAction> _actionQueue;

        private ICharacterAction _lastAction;

        public QueueProcessor(ResettableQueue<ICharacterAction> actionQueue)
        {
            _actionQueue = actionQueue;
        }
        
        public void PerformAction(IInputSetter controllerInput)
        {
            if (_actionQueue.Count == 0) return;
            
            _lastAction = _actionQueue.Dequeue();
            
            controllerInput.PerformInput(_lastAction);
        }

        public void CancelAction(IInputSetter controllerInput)
        {
            if (ReferenceEquals(_lastAction, null)) return;
            controllerInput.CancelInput(_lastAction);
        }
    }
}