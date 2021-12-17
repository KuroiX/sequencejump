﻿using System;

namespace Features.Station
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