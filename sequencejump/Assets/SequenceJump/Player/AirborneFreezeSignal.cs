using SequenceJump.Player.Controller.ControllerParts;
using SequenceJump.Tools;

namespace SequenceJump.Player
{
    public class AirborneFreezeSignal : TimedSignalBehaviour
    {
        private void OnEnable()
        {
            PlatformController.PlatformsTriggered += OnPlatformsTriggered;
        }

        private void OnDisable()
        {
            PlatformController.PlatformsTriggered -= OnPlatformsTriggered;
        }

        private void OnPlatformsTriggered()
        {
            StopAllCoroutines();
            StartCoroutine(StartTriggered());
        }
    }
}