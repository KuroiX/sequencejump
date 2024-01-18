using UnityEngine;
using UnityEngine.UI;

namespace SequenceJump.StationLogic.UI
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
            _button.onClick.AddListener(Station.CurrentStation.Reset);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Station.CurrentStation.Reset);
        }
    }
}