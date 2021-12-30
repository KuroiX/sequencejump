using System;
using UnityEngine.InputSystem;

namespace Features.Player
{
    public interface ICharacterInput
    {
        event Action<InputAction.CallbackContext> ActionPerformed;
        event Action<InputAction.CallbackContext> ActionCanceled;
        
        event Action<InputAction.CallbackContext> MovePerformed;
        event Action<InputAction.CallbackContext> MoveCanceled;
    }
}