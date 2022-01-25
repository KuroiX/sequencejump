using UnityEngine;
using Core.Actions;
using Core.Queue;

namespace Features.StationLogic
{
    public class StationBehaviour : MonoBehaviour
    {
        [SerializeField] private int maxAssignableCount;

        [SerializeField] private int jump;
        [SerializeField] private int dash;
        
        public Station Station => _station;
        public int MaxAssignableCount => maxAssignableCount;

        public int[] ActionCounts
        {
            get
            {
                _actionCounts ??= new[] {jump, dash};
                return _actionCounts;
            }
            set => _actionCounts = value;
        }

        private int[] _actionCounts;
        private Station _station;

        private void Awake()
        {
            _station = new Station(FindObjectOfType<QueueHolder>().Queue, 
                new InstanceCounter<ICharacterAction>(CharacterAction.OrderedActions, ActionCounts), 
                maxAssignableCount);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _station.HandleOnTriggerEnter();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _station.HandleOnTriggerExit();
        }
    }
}
