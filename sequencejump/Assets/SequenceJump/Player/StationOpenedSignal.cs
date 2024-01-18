using System;
using SequenceJump.StationLogic;
using SequenceJump.Tools;

namespace SequenceJump.Player
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