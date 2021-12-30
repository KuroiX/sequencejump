using System;

namespace Foundations.Actions
{
    [Serializable]
    public struct ActionCounter
    {
        public CharacterAction characterAction;
        public int count;
    }
}