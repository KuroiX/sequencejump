namespace Features.Player.Controller.CharacterInput
{
    public class QueueInput
    {
        private readonly QueueProcessor _queueProcessor;
        private readonly ICharacterInput[] _characterInput;

        private readonly IInputSetter _controllerInput;
        
        public QueueInput(QueueProcessor queueProcessor, ICharacterInput[] inputManager, IInputSetter controllerInput)
        {
            _characterInput = inputManager;
            _controllerInput = controllerInput;
            _queueProcessor = queueProcessor;
        }
        
        public void HandleOnEnable()
        {
            foreach (ICharacterInput inputSource in _characterInput)
            {
                inputSource.ActionPerformed += Action;
                inputSource.ActionCanceled += ActionEnd;
            
                inputSource.MovePerformed += Move;
                inputSource.MoveCanceled += Move;
            }
        }
        
        public void HandleOnDisable()
        {
            foreach (ICharacterInput inputSource in _characterInput)
            {
                inputSource.ActionPerformed -= Action;
                inputSource.ActionCanceled -= ActionEnd;
            
                inputSource.MovePerformed -= Move;
                inputSource.MoveCanceled -= Move;
            }
        }
        
        private void Move(float value)
        {
            _controllerInput.SetHorizontal(value);
        }

        private void Action()
        {
            _queueProcessor.PerformAction(_controllerInput);
        }

        private void ActionEnd()
        {
            _queueProcessor.CancelAction(_controllerInput);
        }

        public void HandleLateUpdate()
        {
            _controllerInput.Reset();
        }
    }
}