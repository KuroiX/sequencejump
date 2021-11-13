using UnityEngine;
using UnityEngine.UI;

namespace Features.Station.UI
{
    public class ResetStationButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Station.CurrentStation.ResetStation);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Station.CurrentStation.ResetStation);
        }
    }
}