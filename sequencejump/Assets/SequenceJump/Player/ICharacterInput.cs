using System;

namespace SequenceJump.Player
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