using System;
using UnityEngine;

namespace Features.StationLogic
{
    public class StationRefreshManagerBehaviour : MonoBehaviour
    {
        [SerializeField] private StationEnteredReceiverBehaviour[] stationEnteredReceivers;
        
        private StationBehaviour _stationBehaviour;

        private void Awake()
        {
            _stationBehaviour = GetComponent<StationBehaviour>();
        }

        private void OnEnable()
        {
            Station.StationEntered += OnCurrentStationEntered;
        }

        private void OnDisable()
        {
            Station.StationEntered -= OnCurrentStationEntered;
        }

        private void OnCurrentStationEntered(object sender, EventArgs e)
        {
            if (_stationBehaviour.Station != sender) return;

            foreach (var receiver in stationEnteredReceivers)
            {
                receiver.ReceiveStationEntered();
            }
        }
    }
}