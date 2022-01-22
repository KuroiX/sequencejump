using System;
using Features.StationLogic;
using UnityEngine;

namespace Features.Tutorial
{
    public class DisableUntilRelevantStation : MonoBehaviour
    {
        [SerializeField] private GameObject disableObject;
        
        [SerializeField] private StationBehaviour stationBehaviour;

        private Station _station;

        private void Awake()
        {
            disableObject.SetActive(false);
        }

        private void Start()
        {
            _station = stationBehaviour.Station;
        }

        private void OnEnable()
        {
            Station.StationOpened += OnRelevantStationOpened;
        }

        private void OnRelevantStationOpened(object sender, EventArgs e)
        {
            if (sender == _station)
            {
                disableObject.SetActive(true);
                Destroy(gameObject);
            }
        }

        private void OnDisable()
        {
            Station.StationOpened -= OnRelevantStationOpened;
        }
        
        
    }
}
