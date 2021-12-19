using UnityEngine;

namespace Features.Actions
{
    public interface ICharacterAction
    {
        Sprite Sprite { get; }
        string Name { get; }
    }
}