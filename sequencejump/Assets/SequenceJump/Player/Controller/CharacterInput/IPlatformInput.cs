﻿namespace SequenceJump.Player.Controller.CharacterInput
{
    public interface IPlatformInput
    {
        bool PlatformPerformed { get; }
        bool PlatformCanceled { get; }
    }
}