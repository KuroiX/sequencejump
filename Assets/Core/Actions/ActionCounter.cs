using System;

namespace Core.Actions
{
    [Serializable]
    public struct ActionCounter
    {
        public CharacterAction characterAction;
        public int count;
    }
}