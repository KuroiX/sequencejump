using System;
using UnityEngine.InputSystem;

namespace Features.Player
{
    public interface ICharacterInput
    {
        event Action ActionPerformed;
        event Action ActionCanceled;
        
        event Action<float> MovePerformed;
        event Action<float> MoveCanceled;

        void Enable();

        void Disable();
    }
}