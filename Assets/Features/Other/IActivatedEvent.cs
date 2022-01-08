using System;

namespace Features.Other
{
    public interface IActivatedEvent
    {
        event EventHandler Activated;
    }
}