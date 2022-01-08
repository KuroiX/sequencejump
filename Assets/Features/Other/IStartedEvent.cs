using System;

namespace Features.Other
{
    public interface IStartedEvent
    {
        event EventHandler Started;
    }
}