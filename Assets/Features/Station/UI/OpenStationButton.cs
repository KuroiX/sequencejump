using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Station.UI
{
    public class OpenStationButton : MonoBehaviour
    {
        [SerializeField] private GameObject buttonObject;
        
        private Button _buttonComponent;
        
        private void Awake()
        {
            _buttonComponent = buttonObject.GetComponentInChildren<Button>();
        }
        
        private void OnEnable()
        {
            Station.StationEntered += StationEnteredBehaviour;
            Station.StationExited += StationExitedBehaviour;
            
            Station.StationOpened += StationExitedBehaviour;
            Station.StationClosed += StationEnteredBehaviour;
        }

        private void OnDisable()
        {
            Station.StationEntered -= StationEnteredBehaviour;
            Station.StationExited -= StationExitedBehaviour;
            
            Station.StationOpened -= StationExitedBehaviour;
            Station.StationClosed -= StationEnteredBehaviour;
        }

        private void StationEnteredBehaviour(object sender, EventArgs args)
        {
            _buttonComponent.onClick.AddListener(((StationEventArgs) args).Station.OpenStation);
            buttonObject.SetActive(true);
        }

        private void StationExitedBehaviour(object sender, EventArgs args)
        {
            _buttonComponent.onClick.RemoveListener(((StationEventArgs) args).Station.OpenStation);
            buttonObject.SetActive(false);
        }
    }
}