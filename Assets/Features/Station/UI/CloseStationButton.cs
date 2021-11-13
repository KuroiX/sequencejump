﻿using UnityEngine;
using UnityEngine.UI;

namespace Features.Station.UI
{
    public class CloseStationButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Station.CurrentStation.CloseStation);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Station.CurrentStation.CloseStation);
        }
    }
}