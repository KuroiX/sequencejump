using UnityEngine;

namespace Core.Actions
{
    public interface ICharacterAction
    {
        Sprite Sprite { get; }
        ActionType Type { get; }
    }
}