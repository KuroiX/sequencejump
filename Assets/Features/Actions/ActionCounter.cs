using System;
using UnityEngine;

namespace Features.Actions
{
    [Serializable]
    public struct ActionCounter
    {
        [HideInInspector]
        public CharacterAction CharacterAction;
        public int Count;
    }
}