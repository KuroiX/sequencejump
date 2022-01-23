using System.Collections.Generic;
using UnityEngine;

namespace Core.Actions
{
    [CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Action", order = 1)]
    public class CharacterAction : ScriptableObject, ICharacterAction
    {
        public static Dictionary<string, CharacterAction> CharacterActions { get; private set; }

        [SerializeField] private string actionName;
        public string Name => name;

        [SerializeField] private ActionType type;
        public ActionType Type => type;

        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;

        private void OnEnable()
        {
            CharacterActions ??= new Dictionary<string, CharacterAction>();

            CharacterActions[actionName] = this;
        }
    }
}