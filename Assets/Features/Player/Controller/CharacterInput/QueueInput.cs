namespace Features.Player.Controller.CharacterInput
{
    public class QueueInput
    {
        private readonly QueueProcessor _queueProcessor;
        private readonly ICharacterInput[] _characterInput;

        private readonly IControllerInput _controllerInput;
        
        public QueueInput(QueueProcessor queueProcessor, ICharacterInput[] inputManager, IControllerInput controllerInput)
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
            _controllerInput.Horizontal = value;
        }

        private void Action()
        {
            _queueProcessor.Action(_controllerInput);
        }

        private void ActionEnd()
        {
            _queueProcessor.JumpEnd(_controllerInput);
        }

        public void HandleLateUpdate()
        {
            _controllerInput.JumpPerformed = false;
            _controllerInput.JumpCanceled = false;
            _controllerInput.DashPerformed = false;
        }
    }
}