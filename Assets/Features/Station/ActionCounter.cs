using System.Collections.Generic;
using System.Linq;
using Features.Actions;

namespace Features.Station
{
    public class ActionCounter
    {
        private readonly Dictionary<CharacterAction, int> _availableActions;

        public Dictionary<CharacterAction, int> CurrentAvailableActions;

        public ActionCounter(ActionCount[] count)
        {
            _availableActions = new Dictionary<CharacterAction, int>();
            CurrentAvailableActions = new Dictionary<CharacterAction, int>();
            
            foreach (var value in CharacterAction.CharacterActions.Values)
            {
                _availableActions[value] = 0;
            }

            for (int i = 0; i < count.Length; i++)
            {
                _availableActions[count[i].CharacterAction] = count[i].Count;
            }
            
            ResetCurrentAvailableActions();
        }
        
        public void ResetCurrentAvailableActions()
        {
            foreach (var key in _availableActions.Keys)
            {
                int amount = _availableActions[key];
                CurrentAvailableActions[key] = amount;
            }
        }

        public bool RemoveAction(CharacterAction characterAction)
        {
            bool hasActionLeft = HasActionLeft(characterAction);
            
            if (hasActionLeft)
            {
                CurrentAvailableActions[characterAction]--;
            }
   
            return hasActionLeft;
        }

        public bool HasActionLeft(CharacterAction characterAction)
        {
            return CurrentAvailableActions.Keys.Contains(characterAction) &&
                   CurrentAvailableActions[characterAction] != 0;
        }
    }
}