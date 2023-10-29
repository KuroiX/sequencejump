using Core;
using Core.Queue;
using UnityEngine;

namespace Features.Player.Controller.CharacterInput
{
    public class QueueInputBehaviour : MonoBehaviour, IInputHolder
    {
        public IControllerInput Input => _inputSetter;

        [SerializeField] private TimedSignalBehaviour[] signals;

        private QueueInput _queueInput;
        private InputSourceHandler _inputSourceHandler;
        private readonly ControllerInputSetter _inputSetter = new ControllerInputSetter();

        private void Awake()
        {
            var actionQueue = GetComponent<QueueHolder>().Queue;

            QueueProcessor processor = new QueueProcessor(actionQueue);

            var charInputs = SetupCharacterInputs();

            _queueInput = new QueueInput(processor, charInputs, _inputSetter, signals);
            //_inputSourceHandler = new InputSourceHandler(charInputs, iSignals);
        }

        private ICharacterInput[] SetupCharacterInputs()
        {
            ICharacterInput buttonInput = FindObjectOfType<ButtonWorkaroundInput>();

            bool foundButtonInput = !ReferenceEquals(buttonInput, null);

            ICharacterInput[] characterInputs = new ICharacterInput[foundButtonInput ? 2 : 1];

            characterInputs[0] = new InputManagerInput(new InputManager());

            if (foundButtonInput)
            {
                characterInputs[1] = buttonInput;
            }
            
            foreach (var input in characterInputs)
            {
                input.Enable();
            }

            return characterInputs;
        }
        
        private void OnEnable()
        {
            _queueInput.HandleOnEnable();
            //_inputSourceHandler.HandleOnEnable();
        }

        private void OnDisable()
        {
            _queueInput.HandleOnDisable();
            //_inputSourceHandler.HandleOnDisable();
        }

        private void LateUpdate()
        {
            _queueInput.HandleLateUpdate();
        }
    }
}
