using Cinemachine;
using UnityEngine;
using Core.Actions;
using Core.Queue;

namespace Features.StationLogic
{
    public class StationBehaviour : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int maxAssignableCount;

        [SerializeField] private int jump;
        [SerializeField] private int dash;

        [Header("References")] 
        [SerializeField] private CinemachineVirtualCamera stationCamera;
        [SerializeField] private CinemachineVirtualCamera mainCamera;

        public CinemachineVirtualCamera StationCamera => stationCamera;
        public Station Station => _station;
        public int MaxAssignableCount => maxAssignableCount;

        public int[] ActionCounts
        {
            get
            {
                _actionCounts ??= new[] {jump, dash};
                return _actionCounts;
            }
            set => _actionCounts = value;
        }

        private int[] _actionCounts;
        private Station _station;
        private StationCameraSwitch _cameraSwitch;

        private void Awake()
        {
            _station = new Station(FindObjectOfType<QueueHolder>().Queue, 
                new InstanceCounter<ICharacterAction>(CharacterAction.OrderedActions, ActionCounts), 
                maxAssignableCount);
            _cameraSwitch = new StationCameraSwitch(_station, mainCamera, stationCamera);
        }

        public void Construct(CinemachineVirtualCamera mainCam, int[] actionCounts, int maxCount)
        {
            mainCamera = mainCam;
            _actionCounts = actionCounts;
            maxAssignableCount = maxCount;
        }

        private void OnEnable()
        {
            _cameraSwitch.HandleOnEnable();
        }
        
        private void OnDisable()
        {
            _cameraSwitch.HandleOnDisable();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _station.HandleOnTriggerEnter();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _station.HandleOnTriggerExit();
        }
    }
}
