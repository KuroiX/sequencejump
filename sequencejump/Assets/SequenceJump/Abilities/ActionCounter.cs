using System;

namespace SequenceJump.Abilities
{
    [Serializable]
    public struct ActionCounter
    {
        public CharacterAction characterAction;
        public int count;
    }
}