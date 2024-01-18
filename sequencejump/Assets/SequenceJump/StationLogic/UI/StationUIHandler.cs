using System;
using UnityEngine;

namespace SequenceJump.StationLogic.UI
{
    public class StationUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject stationCanvas;

        private void OnEnable()
        {
            Station.StationOpened += OnStationOpened;
            Station.StationClosed += OnStationClosed;
        }

        private void OnDisable()
        {
            Station.StationOpened -= OnStationOpened;
            Station.StationClosed -= OnStationClosed;
        }

        private void OnStationOpened(object sender, EventArgs e)
        {
            stationCanvas.SetActive(true);
        }

        private void OnStationClosed(object sender, EventArgs e)
        {
            stationCanvas.SetActive(false);
        }
    }
}