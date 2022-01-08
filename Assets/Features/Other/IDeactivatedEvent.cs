using System;

namespace Features.Other
{
    public interface IDeactivatedEvent
    {
        event EventHandler Deactivated;
    }
}