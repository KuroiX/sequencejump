using System;

namespace SequenceJump.Player.Controller.ControllerParts
{
    public class PlatformController
    {
        public static event Action PlatformsTriggered;

        public void Trigger()
        {
            PlatformsTriggered?.Invoke();
        }
    }
}