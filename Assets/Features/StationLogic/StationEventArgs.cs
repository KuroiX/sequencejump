using System;

namespace Features.StationLogic
{
    public class StationEventArgs : EventArgs
    {
        public Station Station { get; }

        public StationEventArgs(Station station)
        {
            Station = station;
        }
    }
}