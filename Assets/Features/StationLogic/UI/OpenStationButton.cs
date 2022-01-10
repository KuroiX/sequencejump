using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.StationLogic.UI
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
            
            Station.StationOpened += StationOpenedBehaviour;
            Station.StationClosed += StationClosedBehaviour;
        }

        private void OnDisable()
        {
            Station.StationEntered -= StationEnteredBehaviour;
            Station.StationExited -= StationExitedBehaviour;
            
            Station.StationOpened -= StationOpenedBehaviour;
            Station.StationClosed -= StationClosedBehaviour;
        }

        private void StationEnteredBehaviour(object sender, EventArgs args)
        {
            _buttonComponent.onClick.AddListener(((Station) sender).Open);
            buttonObject.SetActive(true);
        }

        private void StationExitedBehaviour(object sender, EventArgs args)
        {
            _buttonComponent.onClick.RemoveListener(((Station) sender).Open);
            buttonObject.SetActive(false);
        }
        
        private void StationOpenedBehaviour(object sender, EventArgs args)
        {
            buttonObject.SetActive(false);
        }
        
        private void StationClosedBehaviour(object sender, EventArgs args)
        {
            buttonObject.SetActive(true);
        }
    }
}