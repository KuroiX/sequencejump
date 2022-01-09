using System;

namespace Features
{
    public interface IStartSignal
    {
        event EventHandler Start;
    }
}