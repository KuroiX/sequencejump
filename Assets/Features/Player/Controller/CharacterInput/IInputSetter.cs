﻿using Core.Actions;

namespace Features.Player.Controller.CharacterInput
{
    public interface IInputSetter
    {
        void PerformInput(ICharacterAction action);
        void CancelInput(ICharacterAction action);
        void Reset();
        void SetHorizontal(float value);
    }
}