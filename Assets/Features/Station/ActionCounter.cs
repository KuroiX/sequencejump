using System.Collections.Generic;
using System.Linq;
using Features.Player;

namespace Features.Station
{
    public class ActionCounter
    {
        private readonly Dictionary<Action, int> _availableActions;

        public Dictionary<Action, int> CurrentAvailableActions
        {
            get;
            private set;
        }

        public ActionCounter()
        {
            _availableActions = new Dictionary<Action, int>();
            CurrentAvailableActions = new Dictionary<Action, int>();
        }
        
        private void ResetCurrentAvailableActions()
        {
            foreach (var key in _availableActions.Keys)
            {
                int amount = _availableActions[key];
                CurrentAvailableActions[key] = amount;
            }
        }

        public bool RemoveAction(Action action)
        {
            bool hasActionLeft = HasActionLeft(action);
            
            if (hasActionLeft)
            {
                CurrentAvailableActions[action]--;
            }
   
            return hasActionLeft;
        }

        public bool HasActionLeft(Action action)
        {
            return !CurrentAvailableActions.Keys.Contains(action) &&
                   CurrentAvailableActions[action] != 0;
        }
    }
}