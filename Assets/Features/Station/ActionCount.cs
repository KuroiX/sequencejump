using System;
using Features.Actions;

namespace Features.Station
{
    [Serializable]
    public struct ActionCount
    {
        public CharacterAction CharacterAction;
        public int Count;
    }
}