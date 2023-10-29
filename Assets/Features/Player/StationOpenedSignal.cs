using System;
using Core;
using Features.StationLogic;

namespace Features.Player
{
    public class StationOpenedSignal : TimedSignalBehaviour
    {
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
            OnStartTriggered();
        }
        
        private void OnStationClosed(object sender, EventArgs e)
        {
            OnStopTriggered();
        }
    }
}