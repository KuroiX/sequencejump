using UnityEngine;

namespace SequenceJump.Abilities
{
    public interface ICharacterAction
    {
        Sprite Sprite { get; }
        ActionType Type { get; }
    }
}