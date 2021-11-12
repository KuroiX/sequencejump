using System;
using Features.Actions;
using Features.Queue;
using UnityEngine;

namespace Features.Station
{
    public class StationBehaviour : MonoBehaviour
    {
        public static event EventHandler StationEntered;
        public static event EventHandler StationExited;

        [SerializeField] private StationSettings settings;
        
        private Station _station;

        private void Start()
        {
            _station = new Station(settings, new ResettableQueue<CharacterAction>());
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
