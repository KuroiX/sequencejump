using Features.Actions;
using Features.Queue;
using UnityEngine;

namespace Features.Station
{
    public class StationBehaviour : MonoBehaviour
    {
        [SerializeField] private StationSettings settings;
        
        private Station _station;

        private void Start()
        {
            _station = new Station(settings, new ResettableQueue<CharacterAction>());
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
