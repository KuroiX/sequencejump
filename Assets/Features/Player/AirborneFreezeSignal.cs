using Core;
using Features.Player.Controller.ControllerParts;

namespace Features.Player
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