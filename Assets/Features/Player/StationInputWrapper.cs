using System;
using Features.StationLogic;

namespace Features.Player
{
    public class StationInputWrapper : IStopStartSignal
    {
        public event EventHandler Stop
        {
            add => Station.StationOpened += value;
            remove => Station.StationOpened -= value;
        }
        public event EventHandler Start
        {
            add => Station.StationClosed += value;
            remove => Station.StationClosed -= value;
        }
    }
}