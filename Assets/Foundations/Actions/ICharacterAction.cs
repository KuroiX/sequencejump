using UnityEngine;

namespace Foundations.Actions
{
    public interface ICharacterAction
    {
        Sprite Sprite { get; }
        string Name { get; }
    }
}