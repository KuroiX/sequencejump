using Features.Actions;
using Features.Queue;
using UnityEngine;
using System.Linq;

namespace Features.Station
{
    public class StationBehaviour : MonoBehaviour
    {
        [SerializeField] private StationSettings settings;
        
        private Station _station;

        private void Start()
        {
            _station = new Station(new ResettableQueue<ICharacterAction>(), Create(), settings.maxAssignableActions);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _station.HandleOnTriggerEnter();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            _station.HandleOnTriggerExit();
        }

        private InstanceCounter<ICharacterAction> Create()
        {
            // TODO: UGLY AF, SHOULD BE MOVED TO CUSTOM INSPECTOR ASAP
            int size = CharacterAction.CharacterActions.Count;
            int inputSize = settings.actionCounts.Length;
            
            ICharacterAction[] actions = new ICharacterAction[size];
            int[] count = new int[size];
            
            int i = 0;
            for (; i < inputSize; i++)
            {
                actions[i] = settings.actionCounts[i].characterAction;
                count[i] = settings.actionCounts[i].count;
            }
            
            foreach (var value in CharacterAction.CharacterActions.Values)
            {
                if (!actions.Contains(value))
                {
                    actions[i] = value;
                    count[i] = 0;
                    i++;
                }
            }
            // --------------------------------------------------------

            return new InstanceCounter<ICharacterAction>(actions, count);
        }
    }
}
