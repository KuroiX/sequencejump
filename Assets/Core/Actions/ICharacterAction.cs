using UnityEngine;

namespace Core.Actions
{
    public interface ICharacterAction
    {
        Sprite Sprite { get; }
        string Name { get; }
        ActionType Type { get; }
    }
}