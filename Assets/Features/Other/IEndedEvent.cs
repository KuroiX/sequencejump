using System;

namespace Features.Other
{
    public interface IEndedEvent
    {
        event EventHandler Ended;
    }
}