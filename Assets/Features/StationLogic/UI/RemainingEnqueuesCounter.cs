using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.StationLogic.UI
{
    public class RemainingEnqueuesCounter : MonoBehaviour
    {
        private Image _image;
        private Text _text;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<Text>();
        }

        private void OnEnable()
        {
            if (Station.CurrentStation.HasAssignableCount)
            {
                Station.StationChanged += UpdateText;
                _text.text = Station.CurrentStation.AssignableCount.ToString();
                SetComponents(true);
            }
        }

        private void OnDisable()
        {
            Station.StationChanged -= UpdateText;
            SetComponents(false);
        }

        private void SetComponents(bool value)
        {
            _image.enabled = value;
            _text.enabled = value;
        }

        private void UpdateText(object sender, EventArgs args)
        {
            _text.text = Station.CurrentStation.AssignableCount.ToString();
        }
    }
}