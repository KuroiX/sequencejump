using System;

namespace Features
{
    public interface IStopSignal
    {
        event EventHandler Stop;
    }
}