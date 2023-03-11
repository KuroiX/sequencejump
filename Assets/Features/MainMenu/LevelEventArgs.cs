using System;

namespace Features.MainMenu
{
    public class LevelEventArgs : EventArgs
    {
        public LevelContainer Level { get; }

        public LevelEventArgs(LevelContainer level)
        {
            Level = level;
        }
    }
}