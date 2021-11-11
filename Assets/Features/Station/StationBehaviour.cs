using System;
using UnityEngine;
using Action = Features.Player.Action;

namespace Features.Station
{
    public class StationBehaviour : MonoBehaviour
    {
        public static event EventHandler StationEntered;
        public static event EventHandler StationExited;

        [SerializeField] private ActionCount[] actionCount;
        
        private Station _station;

        private void Start()
        {
            _station = new Station(new ResettableQueue<Action>());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _station.HandleOnTriggerEnter();
            StationEntered?.Invoke(this, new StationEventArgs(_station));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            StationExited?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
