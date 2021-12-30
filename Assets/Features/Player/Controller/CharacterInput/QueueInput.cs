namespace Features.Player.Controller.CharacterInput
{
    public class QueueInput
    {
        private readonly QueueProcessor _queueProcessor;
        private readonly ICharacterInput _characterInput;

        private readonly IControllerInput _controllerInput;
        
        public QueueInput(QueueProcessor queueProcessor, ICharacterInput inputManager, IControllerInput controllerInput)
        {
            _characterInput = inputManager;
            _controllerInput = controllerInput;
            _queueProcessor = queueProcessor;
        }
        
        public void HandleOnEnable()
        {
            _characterInput.ActionPerformed += Action;
            _characterInput.ActionCanceled += ActionEnd;
            
            _characterInput.MovePerformed += Move;
            _characterInput.MoveCanceled += Move;
        }
        
        public void HandleOnDisable()
        {
            _characterInput.ActionPerformed -= Action;
            _characterInput.ActionCanceled -= ActionEnd;
            
            _characterInput.MovePerformed -= Move;
            _characterInput.MoveCanceled -= Move;
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