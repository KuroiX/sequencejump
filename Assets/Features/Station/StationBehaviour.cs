using UnityEngine;

namespace Features.Station
{
    public class StationBehaviour : MonoBehaviour
    {
        private Station _station;
        
        private void Start()
        {
            _station = new Station();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _station.HandleOnTriggerEnter(new ResettableQueue<int>());
        }

        public void OpenStation()
        {
            _station.OpenStation();
        }
    }
}
