using Features.Player.Controller.ControllerParts;
using UnityEngine;

namespace Features.ControllablePlatform
{
    public class StationPlatformManager : MonoBehaviour
    {
        private static StationPlatformManager _currentStationPlatformManager;
        
        [SerializeField] private MovingPlatform[] movingPlatforms;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (ReferenceEquals(_currentStationPlatformManager, this)) return;
            
            if (_currentStationPlatformManager)
            {
                _currentStationPlatformManager.UnsubscribeAll();
            }
            
            SubscribeAll();
            _currentStationPlatformManager = this;
        }

        private void SubscribeAll()
        {
            foreach (var platform in movingPlatforms)
            {
                PlatformController.PlatformsTriggered += platform.MoveToNextPoint;
            }
        }

        private void UnsubscribeAll()
        {
            foreach (var platform in movingPlatforms)
            {
                PlatformController.PlatformsTriggered -= platform.MoveToNextPoint;
            }
        }
    }
}