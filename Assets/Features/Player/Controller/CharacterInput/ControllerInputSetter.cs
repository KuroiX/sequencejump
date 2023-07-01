using System;
using System.Collections.Generic;
using Core.Actions;

namespace Features.Player.Controller.CharacterInput
{
    public class ControllerInputSetter : IControllerInput, IInputSetter
    {
        public float Horizontal { get; private set; }

        public bool JumpPerformed => _inputs[ActionType.Jump].Performed;
        public bool JumpCanceled => _inputs[ActionType.Jump].Canceled;

        public bool DashPerformed => _inputs[ActionType.Dash].Performed;

        public bool AirJumpPerformed => _inputs[ActionType.AirJump].Performed;
        public bool AirJumpCanceled => _inputs[ActionType.AirJump].Canceled;

        public bool PlatformPerformed => _inputs[ActionType.Platform].Performed;
        public bool PlatformCanceled => _inputs[ActionType.Platform].Canceled;

        private readonly Dictionary<ActionType, ActionInput> _inputs;

        public ControllerInputSetter()
        {
            _inputs = new Dictionary<ActionType, ActionInput>();

            foreach (var type in (ActionType[]) Enum.GetValues(typeof(ActionType)))
            {
                _inputs[type] = new ActionInput();
            }
        }

        public void PerformInput(ICharacterAction action)
        {
            _inputs[action.Type].Performed = true;
        }

        public void CancelInput(ICharacterAction action)
        {
            _inputs[action.Type].Canceled = true;
        }

        public void Reset()
        {
            foreach (var action in _inputs.Keys)
            {
                _inputs[action].Performed = false;
                _inputs[action].Canceled = false;
            }
        }

        public void SetHorizontal(float value)
        {
            Horizontal = value;
        }
    }
}