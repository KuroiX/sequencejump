using System.Collections.Generic;
using UnityEngine;

namespace Core.Actions
{
    [CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Action", order = 1)]
    public class CharacterAction : ScriptableObject, ICharacterAction
    {
        public static Dictionary<ActionType, CharacterAction> CharacterActions { get; private set; }
        public static ActionType[] OrderedTypes { get; } = {ActionType.Jump, ActionType.Dash};
        public static ICharacterAction[] OrderedActions {
            get
            {
                ICharacterAction[] actions = new ICharacterAction[OrderedTypes.Length];

                for (int i = 0; i < OrderedTypes.Length; i++)
                {
                    actions[i] = CharacterActions[OrderedTypes[i]];
                }

                return actions;
            }
        }

        [SerializeField] private ActionType type;
        public ActionType Type => type;

        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;

        private void OnEnable()
        {
            CharacterActions ??= new Dictionary<ActionType, CharacterAction>();

            CharacterActions[type] = this;
        }
    }
}