using System.Collections.Generic;
using Features.Player;
using UnityEngine;

namespace Features.Actions
{
    [CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Action", order = 1)]
    public class CharacterAction : ScriptableObject, ICharacterAction
    {
        public static Dictionary<string, CharacterAction> CharacterActions { get; private set; }

        public string Name;

        private void OnEnable()
        {
            CharacterActions ??= new Dictionary<string, CharacterAction>();

            CharacterActions[Name] = this;
        }
    }
}