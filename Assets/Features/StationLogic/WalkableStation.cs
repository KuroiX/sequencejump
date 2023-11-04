using UnityEngine;

namespace Features.StationLogic
{
    public class WalkableStation : MonoBehaviour
    {
        private Station _station;

        [SerializeField] private StationBehaviour stationBehaviour;

        private void Start()
        {
            _station = stationBehaviour.Station;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _station.Open();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _station.Close();
        }
    }
}